"use client";
import { getGuardianshipsWithFilters } from "@/actions/client-utilities/getGuardianshipsWithFilters";
import { textConstants } from "@/domain/globalization/es";
import { IGuardianship } from "@/domain/interfaces/IGuardianship";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import useLazyTable from "@/hooks/useLazyTable";
import useToast from "@/hooks/useToast";
import { Column } from "primereact/column";
import { DataTableRowEditCompleteEvent } from "primereact/datatable";
import { Toast } from "primereact/toast";
import { useCallback, useEffect, useState, useTransition } from "react";
import Table from "../common/Table";
import ReassignGuardianship from "./ReassignGuardianship";
import Link from "next/link";
import Image from "next/image";
import { ConfirmDialog, confirmDialog } from "primereact/confirmdialog";
import { useSearchParams } from "next/navigation";
import { EProcessStatus } from "../../domain/enums/EProcessStatus";
import { updateCaseStatus } from "@/actions/case-status/updateCaseStatus";
import { assignCase } from "@/actions/case-assignment/assignCase";
import { ITeamMemberAssignment } from "@/domain/interfaces/ITeamMember";
import { DATES, TABLE_FILTER_CONSTANTS } from "@/domain/constants/components";
import { getTeamMembersByTeamLeaderId } from "@/actions/client-utilities/getTeamMembersByTeamLeaderId";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { getTableFilterTemplate } from "@/services/getTableFilterTemplate";
import { usePermissions } from "@/hooks/usePermissions";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";
import { ERoles } from "@/domain/enums/ERoles";
import { EProcessTemplates } from "@/domain/enums/EProcessTemplates";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import { getOnlyDaysDifference } from "@/services/getOnlyDaysDifference";
import { generateTemplate } from "@/actions/generate-template/generateTemplate";
import useFileDownload from "@/hooks/useFileDownload";

const GuardianshipsTable = () => {
  const { validatePermission, permissionConstants } = usePermissions();
  const { toastRef, showToast } = useToast();
  const [isPending, startTransition] = useTransition();
  const [guardianships, setGuardianships] = useState<
    IGuardianship[] | undefined
  >();
  const user = useCurrentUser();
  const [teamMembers, setTeamMembers] = useState<ITeamMemberAssignment[]>();
  const {
    lazyState,
    totalRecords,
    setTotalRecords,
    isLoading,
    setIsLoading,
    onPage,
    onSort,
    onFilter,
    setRows,
    matchModeEqualsOptions,
  } = useLazyTable([
    "submissionDate",
    "deadLineDate",
    "assignedTeamMemberName",
    "assignedTeamMemberId",
    "assignment.teamMember.memberId",
  ]);
  const searchParams = useSearchParams();
  const debouncedValue = searchParams.get("query") || "";
  const { headers, rows } = textConstants.pages.guardianships.table;
  const altText = textConstants.components.alt.icons;
  const statusText = textConstants.processStatus;
  const [isDownloadLoading, setIsDownloadLoading] = useState(false);
  const { automaticDownload } = useFileDownload();

  const getGuardianshipsAndAssignmentsCallback = useCallback(
    async (debouncedValue: string, lazyState: ILazyTableState) => {
      setIsLoading(true);
      const addAssignedMemberId = (guardianships: IGuardianship[]) => {
        return guardianships.map((guardianship) => {
          return {
            ...guardianship,
            assignment: { teamMember: { memberId: guardianship?.memberId } },
          };
        });
      };
      getGuardianshipsWithFilters({ lazyState, debouncedValue })
        .then((response) => {
          response?.data
            ? setGuardianships(
                addAssignedMemberId(response?.data as IGuardianship[])
              )
            : setGuardianships([]);
          response?.totalCount && setTotalRecords(response?.totalCount);
        })
        .finally(() => {
          setIsLoading(false);
        });
    },

    [setIsLoading, setTotalRecords]
  );

  const getTeamMembersCallback = useCallback(async () => {
    validatePermission(permissionConstants.guardianships.assign) &&
      getTeamMembersByTeamLeaderId(Number(user?.id)).then((response) => {
        response ? setTeamMembers(response) : setTeamMembers([]);
      });
  }, [permissionConstants.guardianships.assign, user?.id, validatePermission]);

  useEffect(() => {
    getGuardianshipsAndAssignmentsCallback(debouncedValue, lazyState);
    getTeamMembersCallback();
  }, [
    debouncedValue,
    getGuardianshipsAndAssignmentsCallback,
    getTeamMembersCallback,
    lazyState,
  ]);

  const onRowEditComplete = async (e: DataTableRowEditCompleteEvent) => {
    let { newData, index } = e;
    const newGuardianship = {
      ...newData,
      assignedTeamMemberName: teamMembers?.find(
        (assignment) =>
          assignment.memberId === newData.assignment.teamMember.memberId
      )?.fullName,
      memberId: newData.assignment.teamMember.memberId,
    } as IGuardianship;

    //validate if there was an update
    if (
      !newGuardianship?.memberId ||
      (guardianships &&
        newGuardianship?.memberId === guardianships[index]?.memberId)
    ) {
      //if there is no update then not update the view
      return;
    }

    //if there is an update then update the view
    setGuardianships((prev) => {
      const updatedGuardianships = [...prev!];
      updatedGuardianships[index] = newGuardianship;
      return updatedGuardianships;
    });

    //update case status to asigned if the status is less than asigned
    const nextStatus =
      newGuardianship?.statusId &&
      EProcessStatus.Asignada < newGuardianship?.statusId
        ? newGuardianship?.statusId
        : EProcessStatus.Asignada;

    await startTransition(() => {
      assignCase({
        caseId: newGuardianship.id,
        userId: newGuardianship.memberId,
        caseStatusId: nextStatus,
      }).then((response) => {
        showToast(response);
        response.toastType === "success" &&
          getGuardianshipsAndAssignmentsCallback(debouncedValue, lazyState);
        //if there is an error, revert changes in the view
        response.toastType === "error" && setGuardianships(guardianships);
      });
    });
  };

  const assignedToBodyTemplate = (rowData: IGuardianship) => {
    return (
      <span
        className={`${
          !rowData?.assignedTeamMemberName
            ? "text-[--papirus-grey] uppercase"
            : ""
        } `}>
        {rowData?.assignedTeamMemberName || rows.assignedTo.notAssigned}
      </span>
    );
  };

  const statusBodyTemplate = (rowData: IGuardianship) => {
    return (
      <span>
        {rowData?.statusId
          ? statusText.status[rowData?.statusId as EProcessStatus]
          : statusText.status[1]}
      </span>
    );
  };

  const submissionDateTemplate = (rowData: IGuardianship) => {
    return rowData?.submissionDate ? (
      <p className="p-1 w-fit">
        {rowData?.submissionDate
          ? new Date(rowData?.submissionDate).toLocaleString(
              DATES.localeDateString
            )
          : ""}
      </p>
    ) : null;
  };

  const deadLineDateTemplate = (rowData: IGuardianship) => {
    const daysDifference = rowData?.deadLineDate
      ? getOnlyDaysDifference(rowData?.deadLineDate)
      : 0;

    let deadLine: "today" | "tomorrow" | "overdue" | "upcoming" | "completed";

    if (rowData?.statusId === EProcessStatus.Contestada) {
      deadLine = "completed";
    } else if (daysDifference >= 1 && daysDifference < 2) {
      deadLine = "tomorrow";
    } else if (daysDifference < 0) {
      deadLine = "overdue";
    } else if (daysDifference >= 0 && daysDifference < 1) {
      deadLine = "today";
    } else {
      deadLine = "upcoming";
    }

    const getdeadLineStyles = (deadLine: string) => {
      switch (deadLine) {
        case "overdue":
          return "bg-[--guardianship-overdue] text-[--white]";
        case "today":
          return "bg-[--guardianship-today] text-[--white]";
        case "tomorrow":
          return "bg-[--guardianship-tomorrow] text-[--black]";
        case "upcoming":
          return "bg-[--guardianship-upcoming] text-[--white]";
        case "completed":
          return "bg-[--icon-grey-background] text-[--black]";
        default:
          break;
      }
    };

    return rowData?.deadLineDate ? (
      <p className={`rounded-md p-1 w-fit ${getdeadLineStyles(deadLine)}`}>
        {rowData?.deadLineDate
          ? new Date(rowData?.deadLineDate).toLocaleString(
              DATES.localeDateString
            )
          : ""}
      </p>
    ) : null;
  };

  const onDownload = async (rowData: IGuardianship) => {
    setIsDownloadLoading(true);
    const documentToDownload = rowData.isAnswered
      ? {
          templateId: EProcessTemplates.ResponseDocument,
          documentType: EGuardianshipDocumentTypes.ResponseDocument,
        }
      : {
          templateId: EProcessTemplates.EmergencyBrief,
          documentType: EGuardianshipDocumentTypes.EmergencyBrief,
        };

    (rowData.isAnswered || rowData.emergencyBriefAnswered) &&
      (await generateTemplate({
        caseId: rowData.id || 0,
        ...documentToDownload,
      }).then(async (responseState) => {
        await automaticDownload({
          path: responseState.data?.filePath,
          name: responseState.data?.fileName,
        });
        showToast(responseState);
      }));
    setIsDownloadLoading(false);
  };

  const _accept = async () => {
    toastRef.current?.show({
      severity: "success",
      summary: "Tutelas",
      detail: "Se cambió el estado de la tutela a Contestada",
      life: 3000,
    });
  };

  const reject = () => {
    toastRef.current?.show({
      severity: "warn",
      summary: "Tutelas",
      detail: "No se realizaron cambios",
      life: 3000,
    });
  };

  const confirmReplied = async (rowData: IGuardianship) => {
    let accept = async () => {
      await updateCaseStatus({
        caseId: rowData?.id || 0,
        userId: rowData?.memberId || 0,
        currentStatus: rowData?.statusId || 1,
        nextStatus: EProcessStatus.Contestada,
      }).then((res) => {
        res.continue && _accept();
        getGuardianshipsAndAssignmentsCallback(debouncedValue, lazyState);
      });
    };

    confirmDialog({
      message: "¿Desea marcar esta tutela como contestada?",
      header: "Tutelas",
      icon: "pi pi-reply",
      defaultFocus: "accept",
      accept,
      reject,
      acceptLabel: "Sí",
      rejectLabel: "No",
    });
  };

  const getAssignedToColumnFilterOptions = () => {
    const disponibleListForFilter =
      teamMembers && teamMembers.length > 0
        ? teamMembers.map((assignment) => {
            return {
              value: assignment.memberId || 0,
              label: assignment.fullName || "",
            };
          })
        : [
            {
              value: Number(user?.id) || 0,
              label: user?.name || "",
            },
          ];
    return [
      {
        value: TABLE_FILTER_CONSTANTS.dropdownEmptyIndentifier,
        label: rows.assignedTo.notAssigned,
      },
      ...disponibleListForFilter,
    ];
  };

  return (
    <div className="flex items-center justify-between bg-[--white] w-full rounded-md shadow-md">
      <Toast ref={toastRef} />
      <ConfirmDialog />
      <Table
        id="guardianships-data-table"
        dataKey="id"
        rowEditorInitIcon={
          <Image
            src="/reassign.svg"
            alt={altText.reassign}
            width={16}
            height={13}
          />
        }
        rowEditorSaveIcon={
          <Image src="/check.svg" alt={altText.save} width={15} height={12} />
        }
        rowEditorCancelIcon={
          <Image src="/cross.svg" alt={altText.cancel} width={14} height={14} />
        }
        onRowEditComplete={onRowEditComplete}
        value={guardianships}
        loading={isLoading || isPending || isDownloadLoading}
        rows={lazyState.rows}
        first={lazyState.first}
        totalRecords={totalRecords}
        onPage={onPage}
        onSort={onSort}
        sortField={lazyState.sortField}
        sortOrder={lazyState.sortOrder}
        onFilter={onFilter}
        filters={lazyState.filters}
        onRowsPerPageChange={setRows}
        onRowEditInit={() => {
          validatePermission(permissionConstants.guardianships.assign) &&
            getTeamMembersCallback();
        }}>
        <Column
          field="submissionDate"
          header={headers.submissionDate}
          body={submissionDateTemplate}
          sortable
          pt={{
            sort: {
              id: `submissionDate-sort`,
            },
          }}
          bodyClassName={"w-[10rem]"}
        />
        <Column
          field="deadLineDate"
          header={headers.deadLineDate}
          body={deadLineDateTemplate}
          sortable
          pt={{
            sort: {
              id: `deadLineDate-sort`,
            },
          }}
          bodyClassName={"w-[10rem]"}
        />
        <Column field="defendantName" header={headers.defendantName} />
        <Column field="claimerName" header={headers.claimerName} />
        <Column
          field="submissionIdentifier"
          header={headers.submissionIdentifier}
        />
        <Column
          field="status"
          header={headers.status}
          bodyClassName={"w-[6rem]"}
          body={statusBodyTemplate}
        />
        <Column
          field="assignment.teamMember.memberId"
          header={headers.assignedTo}
          body={assignedToBodyTemplate}
          editor={(options) => (
            <ReassignGuardianship
              editorOptions={options}
              dropdownOptions={teamMembers}
              emptyText={rows.assignedTo.notAssigned}
            />
          )}
          showFilterOperator
          bodyClassName={"w-[10rem]"}
          {...getTableFilterTemplate({
            id: "assignment.teamMember.memberId",
            filterVariant: "dropdown",
            dropdownOptions: getAssignedToColumnFilterOptions(),
            label: headers.assignedTo,
            placeholder: headers.assignedTo,
            filterMatchModeOptions: matchModeEqualsOptions,
          })}
        />
        <Column
          headerClassName="min-w-[72px] w-[6%]"
          bodyClassName="text-center h-full p-1"
          body={(rowData: IGuardianship) => {
            let _confirmReplied = () => confirmReplied(rowData);
            return (
              <div className="flex justify-center w-full h-full items-center gap-2">
                <Link
                  id={`redirect-to-guardianship-${rowData?.id}-a`}
                  href={`/guardianships/${rowData?.id}/documents`}
                  className="papirus-icon-button"
                  {...getDisabledLinkProps(
                    !rowData.isCurrentAssigned &&
                      user?.roleId !== ERoles.SuperAdmin &&
                      user?.roleId !== ERoles.Lead
                  )}>
                  <Image
                    src="/redirect.svg"
                    alt={altText.access}
                    width={13}
                    height={13}
                  />
                </Link>
                <button
                  id={`download-guardianship-${rowData?.id}-document-btn`}
                  className="papirus-icon-button"
                  disabled={
                    (!rowData.isAnswered && !rowData?.emergencyBriefAnswered) ||
                    !validatePermission(
                      permissionConstants.guardianships.download
                    )
                  }
                  onClick={() => onDownload(rowData)}>
                  <Image
                    src="/download.svg"
                    alt={altText.edit}
                    width={16}
                    height={13}
                  />
                </button>
                <button
                  id={`mark-guardianship-${rowData?.id}-as-replied-btn`}
                  className="papirus-icon-button"
                  disabled={
                    !rowData.isAnswered ||
                    (!rowData.isCurrentAssigned &&
                      user?.roleId !== ERoles.SuperAdmin) ||
                    rowData?.statusId !== EProcessStatus.EnProgreso ||
                    (!validatePermission(
                      permissionConstants.guardianships.answered
                    ) &&
                      !validatePermission(
                        permissionConstants.guardianships.download
                      ))
                  }
                  onClick={_confirmReplied}>
                  <i className="pi pi-reply text-white"></i>
                </button>
              </div>
            );
          }}
        />
        <Column
          headerClassName="min-w-[80px] w-[6%]"
          rowEditor={true}
          bodyClassName="h-full p-1"
          pt={{
            rowEditorCancelButton: (options) => ({
              className: "papirus-icon-button inline-flex h-[28px] w-[28px]",
              id: `cancel-reassign-${options?.state.editingRowData?.id}-btn`,
              ...getDisabledLinkProps(
                !validatePermission(permissionConstants.guardianships.assign)
              ),
            }),
            rowEditorSaveButton: (options) => ({
              className:
                "papirus-icon-button inline-flex h-[28px] w-[28px] mr-2",
              id: `save-reassign-${options?.state.editingRowData?.id}-btn`,
              ...getDisabledLinkProps(
                !validatePermission(permissionConstants.guardianships.assign)
              ),
            }),
            rowEditorInitButton: (options) => ({
              className: "papirus-icon-button w-[28px] h-[28px]",
              id: `reassign-case-${options?.state.editingRowData?.id}-btn`,
              ...getDisabledLinkProps(
                !validatePermission(permissionConstants.guardianships.assign)
              ),
            }),
          }}
        />
      </Table>
    </div>
  );
};

export default GuardianshipsTable;
