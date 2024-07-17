"use client";

import { getTeamsWithFilters } from "@/actions/client-utilities/getTeamsWithFilters";
import { textConstants } from "@/domain/globalization/es";
import { ITeam } from "@/domain/interfaces/ITeam";
import { Column } from "primereact/column";
import { useCallback, useEffect, useRef, useState } from "react";
import { Montserrat } from "next/font/google";
import { Button } from "primereact/button";
import { ConfirmDialog, confirmDialog } from "primereact/confirmdialog";
import { Toast } from "primereact/toast";
import { Dialog } from "primereact/dialog";
import TeamsForm from "./TeamsForm";
import { deleteTeam } from "@/actions/teams/deleteTeam";
import { ITeamMember } from "@/domain/interfaces/ITeamMember";
import { saveTeamWithDetails } from "@/actions/teams/saveTeamWithDetails";
import Table from "../common/Table";
import useLazyTable from "@/hooks/useLazyTable";
import { getTableFilterTemplate } from "@/services/getTableFilterTemplate";
import { usePermissions } from "@/hooks/usePermissions";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";

const TeamsTable = ({
  debouncedValue,
  showEditDialog,
  team,
  setTeam,
  teamMembers,
  setTeamMembers,
}: {
  debouncedValue: string;
  showEditDialog?: any;
  team: ITeam | undefined;
  setTeam: React.Dispatch<React.SetStateAction<ITeam | undefined>>;
  teamMembers: ITeamMember[] | undefined[] | undefined;
  setTeamMembers: React.Dispatch<
    React.SetStateAction<ITeamMember[] | undefined[] | undefined>
  >;
}) => {
  const [teams, setTeams] = useState<ITeam[] | undefined>();
  const [reload, setReload] = useState<Date>(new Date());
  const [originalTeam, setOriginalTeam] = useState<ITeam | undefined>(
    undefined
  );
  const [originalTeamMembers, setOriginalTeamMembers] = useState<
    ITeamMember[] | undefined[] | undefined
    >(undefined);
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
    matchModeTextOptions,
  } = useLazyTable(["name"]);

  const toast = useRef<Toast>(null);

  const actionBodyTemplate = (rowData: ITeam) => {
    return (
      <div className="action-buttons flex gap-2.5">
        <Button
          id={`edit-team-${rowData.id}-btn`}
          type="button"
          icon="pi pi-pencil text-white"
          severity="warning"
          rounded
          disabled={!validatePermission(permissionConstants.teams.edit)}
          onClick={() => edit(rowData)}></Button>
        <Button
          id={`delete-team-${rowData.id}-btn`}
          type="button"
          icon="pi pi-trash text-white"
          rounded
          severity="danger"
          disabled={!validatePermission(permissionConstants.teams.delete)}
          onClick={() => confirmDelete(rowData)}></Button>
      </div>
    );
  };

  const edit = (rowData: ITeam) => {
    setTeam(rowData);
    showEditDialog.setTrue();
  };

  const confirmDelete = (rowData: ITeam) => {
    const deleteHandler = async () => {
      const teamId = Number.parseInt(rowData.id.toString());
      const deleteTeamResponse = await deleteTeam(teamId);

      toast.current?.show({
        severity: deleteTeamResponse?.message ? "success" : "error",
        summary: "Equipos",
        detail: deleteTeamResponse?.message ?? deleteTeamResponse?.error,
        life: 3000,
      });

      setReload(new Date());
    };

    confirmDialog({
      message: "¿Realmente desea eliminar este equipo?",
      header: rowData.name,
      icon: "pi pi-info-circle",
      defaultFocus: "reject",
      acceptClassName: "p-button-danger",
      accept: deleteHandler,
    });
  };

  const getTeamsCallback = useCallback(
    async (debouncedValue: string, lazyState: ILazyTableState) => {
      setIsLoading(true);
      getTeamsWithFilters(lazyState, debouncedValue)
        .then((response) => {
          response?.data ? setTeams(response?.data) : setTeams([]);
          response?.totalCount && setTotalRecords(response?.totalCount);
        })
        .catch((error) => {
          throw error;
        })
        .finally(() => {
          setIsLoading(false);
        });
    },

    [setIsLoading, setTotalRecords]
  );

  useEffect(() => {
    getTeamsCallback(debouncedValue, lazyState);
  }, [debouncedValue, getTeamsCallback, lazyState, reload]);

  useEffect(() => {
    if (team && !originalTeam) setOriginalTeam(team);
    if (teamMembers && !originalTeamMembers)
      setOriginalTeamMembers(teamMembers);
  }, [team, teamMembers, originalTeam, originalTeamMembers]);

  const dialogHeader = team?.id ? "Editar equipo" : "Crear equipo";

  const cancelForm = () => {
    showEditDialog.setFalse();
    setTeam(undefined);
    setTeamMembers(undefined);
    setOriginalTeam(undefined);
    setOriginalTeamMembers(undefined);
  };
  const saveForm = async () => {
    const ret = await saveTeamWithDetails(
      team,
      teamMembers,
      originalTeam,
      originalTeamMembers
    );

    if (ret?.severity == "success") {
      cancelForm();
      setReload(new Date());
    }

    toast.current?.show(ret!);
  };

  return (
    <div className="flex items-center justify-between bg-[--white] w-full rounded-lg shadow-md">
      <ConfirmDialog className={montserratFont.className} />
      <Toast ref={toast} />
      <Dialog
        className={montserratFont.className + " w-full mx-3 md:max-w-4xl"}
        header={dialogHeader}
        onHide={cancelForm}
        visible={showEditDialog.isTrue}>
        <TeamsForm
          team={team}
          setTeam={setTeam}
          teamMembers={teamMembers}
          setTeamMembers={setTeamMembers}
          saveForm={saveForm}
          cancelForm={cancelForm}
        />
      </Dialog>
      <Table
        id="teams-data-table"
        value={teams}
        loading={isLoading}
        rows={lazyState.rows}
        onRowsPerPageChange={setRows}
        onPage={onPage}
        onSort={onSort}
        onFilter={onFilter}
        totalRecords={totalRecords}
        sortField={lazyState.sortField}
        sortOrder={lazyState.sortOrder}
        tableStyle={tableStyle}
        filters={lazyState.filters}>
        <Column
          field="name"
          header={headers.name}
          sortable
          {...getTableFilterTemplate({
            id: "firstName",
            placeholder: headers.name,
            label: headers.name,
            inputType: "text",
            filterVariant: "text",
            filterMatchModeOptions: matchModeTextOptions,
          })}
        />
        <Column
          headerStyle={{ width: "5rem", textAlign: "center" }}
          bodyStyle={{ textAlign: "center", overflow: "visible" }}
          body={actionBodyTemplate}
        />
      </Table>
    </div>
  );
};

export default TeamsTable;

const montserratFont = Montserrat({ subsets: ["latin"] });
const { headers } = textConstants.pages.teams.table;
const tableStyle = { minWidth: "10rem" };
