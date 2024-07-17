"use client";
import { textConstants } from "@/domain/globalization/es";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import useLazyTable from "@/hooks/useLazyTable";
import useToast from "@/hooks/useToast";
import { Column } from "primereact/column";
import { DataTableRowEditCompleteEvent } from "primereact/datatable";
import { Toast } from "primereact/toast";
import { useCallback, useEffect, useState, useTransition } from "react";
import Table from "../common/Table";
import ReassignCase from "./ReassignCase";
import Link from "next/link";
import Image from "next/image";
import { ICase } from "@/domain/interfaces/ICase";
import { ITeamMemberAssignment } from "@/domain/interfaces/ITeamMember";
import { getCases } from "@/actions/cases/getCases";
import { getTimeDifference } from "@/services/getTimeDifference";
import { useSearchParams } from "next/navigation";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";
import { ConfirmDialog, confirmDialog } from "primereact/confirmdialog";
import { EProcessStatus } from "@/domain/enums/EProcessStatus";
import { updateCaseStatus } from "@/actions/case-status/updateCaseStatus";
import { getTableFilterTemplate } from "@/services/getTableFilterTemplate";
import { getTeamMembersByTeamLeaderId } from "@/actions/teamMembers/getTeamMembersByTeamLeaderId";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { assignCase } from "@/actions/case-assignment/assignCase";
import { TABLE_FILTER_CONSTANTS } from "@/domain/constants/components";
import { usePermissions } from "@/hooks/usePermissions";

const CasesTable = () => {
  const { toastRef, showToast } = useToast();
  const user = useCurrentUser();
  const [isPending, startTransition] = useTransition();
  const [cases, setCases] = useState<ICase[] | undefined>();
  const [assignments, setAssignments] = useState<
    ITeamMemberAssignment[] | undefined
    >();
  const { validatePermission, permissionConstants } = usePermissions();
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
  ]);
  const searchParams = useSearchParams();
  const debouncedValue = searchParams.get("query") || "";
  

  const { headers, rows } = textConstants.pages.guardianships.table;
  const altText = textConstants.components.alt.icons;
  const statusText = textConstants.processStatus;

  const getCasesAndAssignmentsCallback = useCallback(
    async (debouncedValue: string, lazyState: ILazyTableState) => {
      setIsLoading(true);
      getCases({ lazyState, debouncedValue })
        .then((dbCases) => {
          dbCases ? setCases(dbCases?.data) : setCases([]);
          dbCases?.totalCount && setTotalRecords(dbCases.totalCount);
        })
        .finally(() => {
          setIsLoading(false);
        });
      user?.id &&
        getTeamMembersByTeamLeaderId(Number(user?.id))
          .then((temMembers) => {
            temMembers ? setAssignments(temMembers) : setAssignments([]);
          })
          .finally(() => {
            setIsLoading(false);
          });
    },

    [setIsLoading, setTotalRecords, user?.id]
  );

  useEffect(() => {
    getCasesAndAssignmentsCallback(debouncedValue, lazyState);
  }, [debouncedValue, getCasesAndAssignmentsCallback, lazyState]);

  const onRowEditComplete = async (e: DataTableRowEditCompleteEvent) => {
    let { newData, index } = e;
    const newCase = {
      ...newData,
      assignedTeamMemberName: assignments?.find(
        (assignment) => assignment.memberId === newData.assignedTeamMemberId
      )?.fullName,
    } as ICase;
    //validate if there was an update
    if (
      !newCase?.assignedTeamMemberId ||
      (cases &&
        newCase?.assignedTeamMemberId === cases[index]?.assignedTeamMemberId)
    ) {
      //if there is no update then not update the view
      return;
    }

    //if there is an update then update the view
    setCases((prev) => {
      const updatedCases = [...prev!];
      updatedCases[index] = newCase;
      return updatedCases;
    });

    //update case status to asigned
    const nextStatus =
      newCase?.statusId && EProcessStatus.Asignada > newCase?.statusId
        ? newCase?.statusId
        : EProcessStatus.Asignada;

    await startTransition(() => {
      assignCase({
        caseId: newCase.id,
        userId: newCase?.memberId,
        caseStatusId: nextStatus,
      }).then((response) => {
        showToast(response);
        response.toastType === "success" &&
          getCasesAndAssignmentsCallback(debouncedValue, lazyState);
        //if there is an error, revert changes in the view
        response.toastType === "error" && setCases(cases);
      });
    });
  };

  const assignedTeamMemberBodyTemplate = (rowData: ICase) => {
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

  const statusBodyTemplate = (rowData: ICase) => {
    return (
      <span>
        {rowData?.statusId
          ? statusText.status[rowData?.statusId]
          : statusText.status[1]}
      </span>
    );
  };

  const submissionDateTemplate = (rowData: ICase) => {
    return (
      <p className="p-1 w-fit">
        {rowData?.submissionDate
          ? new Date(rowData?.submissionDate).toLocaleString()
          : ""}
      </p>
    );
  };

  const deadLineDateTemplate = (rowData: ICase) => {
    const daysDifference = rowData?.deadLineDate
      ? getTimeDifference(rowData?.deadLineDate)?.days
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

    return (
      <p className={`rounded-md p-1 w-fit ${getdeadLineStyles(deadLine)}`}>
        {rowData?.deadLineDate
          ? new Date(rowData?.deadLineDate).toLocaleString()
          : ""}
      </p>
    );
  };

  const onDownload = async (rowData: ICase) => {
    const downloadDocument = (url: string, name: string) => {
      const a = document.createElement("a");
      a.href = url;
      a.download = name;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
    };

    rowData?.filePath &&
      rowData?.fileName &&
      downloadDocument(rowData?.filePath, rowData?.fileName);
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

  const confirmReplied = async (rowData: ICase) => {
    let accept = async () => {
      await updateCaseStatus({
        caseId: rowData.id,
        userId: rowData?.memberId || 0,
        currentStatus: rowData?.statusId,
        nextStatus: EProcessStatus.Contestada,
      }).then((res) => {
        res.continue && _accept();
        getCasesAndAssignmentsCallback(debouncedValue, lazyState);
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
    return [
      {
        value: TABLE_FILTER_CONSTANTS.dropdownEmptyIndentifier,
        label: rows.assignedTo.notAssigned,
      },
      ...(assignments?.map((assignment) => {
        return {
          value: assignment.memberId!,
          label: assignment.fullName!,
        };
      }) || []),
    ];
  };

  return (
    <div className="flex items-center justify-between bg-[--white] w-full rounded-md shadow-md">
      <Toast ref={toastRef} />
      <ConfirmDialog />
      <Table
        id="cases-data-table"
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
        value={cases}
        loading={isLoading || isPending}
        rows={lazyState.rows}
        first={lazyState.first}
        totalRecords={totalRecords}
        onPage={onPage}
        onSort={onSort}
        sortField={lazyState.sortField}
        sortOrder={lazyState.sortOrder}
        onFilter={onFilter}
        filters={lazyState.filters}
        onRowsPerPageChange={setRows}>
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
          field="assignedTeamMemberId"
          header={headers.assignedTo}
          body={assignedTeamMemberBodyTemplate}
          editor={(options) => (
            <ReassignCase
              editorOptions={options}
              dropdownOptions={assignments}
              emptyText={rows.assignedTo.notAssigned}
            />
          )}
          showFilterOperator
          bodyClassName={"w-[10rem]"}
          {...getTableFilterTemplate({
            id: "assignedTeamMemberId",
            filterVariant: "dropdown",
            dropdownOptions: getAssignedToColumnFilterOptions(),
            label: headers.assignedTo,
            placeholder: headers.assignedTo,
            filterMatchModeOptions: matchModeEqualsOptions,
          })}
        />
        <Column
          headerClassName="min-w-[72px] w-[6%]"
          rowEditor={true}
          bodyClassName="text-center h-full p-1"
          body={(rowData: ICase) => {
            return (
              <div className="flex justify-center w-full h-full items-center gap-2">
                <Link
                  id={`redirect-to-case-${rowData?.id}-a`}
                  href={`/guardianships/${rowData?.id}/documents`}
                  className="papirus-icon-button"
                  {...getDisabledLinkProps(
                    !validatePermission(permissionConstants.documents.view)
                  )}>
                  <Image
                    src="/redirect.svg"
                    alt={altText.access}
                    width={13}
                    height={13}
                  />
                </Link>
                <button
                  id={`download-case-${rowData?.id}-document-btn`}
                  className="papirus-icon-button"
                  disabled={
                    !rowData.filePath ||
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
                  id={`mark-case-${rowData?.id}-as-replied-btn`}
                  className="papirus-icon-button"
                  disabled={
                    !rowData.filePath ||
                    !validatePermission(
                      permissionConstants.guardianships.answered
                    )
                  }
                  onClick={() => confirmReplied(rowData)}>
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
              "aria-disabled": !validatePermission(
                permissionConstants.guardianships.assign
              ),
              style: {
                pointerEvents: !validatePermission(
                  permissionConstants.guardianships.assign
                )
                  ? "none"
                  : "auto",
              },
              tabIndex: !validatePermission(
                permissionConstants.guardianships.assign
              )
                ? -1
                : undefined,
            }),
            rowEditorSaveButton: (options) => ({
              className:
                "papirus-icon-button inline-flex h-[28px] w-[28px] mr-2",
              id: `save-reassign-${options?.state.editingRowData?.id}-btn`,
              "aria-disabled": !validatePermission(
                permissionConstants.guardianships.assign
              ),
              style: {
                pointerEvents: !validatePermission(
                  permissionConstants.guardianships.assign
                )
                  ? "none"
                  : "auto",
              },
              tabIndex: !validatePermission(
                permissionConstants.guardianships.assign
              )
                ? -1
                : undefined,
            }),
            rowEditorInitButton: (options) => ({
              className: "papirus-icon-button w-[28px] h-[28px]",
              id: `reassign-case-${options?.state.editingRowData?.id}-btn`,
              "aria-disabled": !validatePermission(
                permissionConstants.guardianships.assign
              ),
              style: {
                pointerEvents: !validatePermission(
                  permissionConstants.guardianships.assign
                )
                  ? "none"
                  : "auto",
              },
              tabIndex: !validatePermission(
                permissionConstants.guardianships.assign
              )
                ? -1
                : undefined,
            }),
          }}
        />
      </Table>
    </div>
  );
};

export default CasesTable;
