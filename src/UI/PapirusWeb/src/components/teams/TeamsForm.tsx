"use client";
import { getTeamMembersByTeamId } from "@/actions/teamMembers/getTeamMembersByTeamId";
import { getUsersWithFilters } from "@/actions/client-utilities/getUsersWithFilters";
import { TeamMemberDto, UserDto } from "@/domain/entities/data-contracts";
import { ITeam } from "@/domain/interfaces/ITeam";
import { ITeamMember } from "@/domain/interfaces/ITeamMember";
import {
  AutoComplete,
  AutoCompleteChangeEvent,
  AutoCompleteCompleteEvent,
  AutoCompletePassThroughOptions,
  AutoCompleteSelectEvent,
} from "primereact/autocomplete";
import { Column, ColumnBodyOptions } from "primereact/column";
import { DataTableValueArray } from "primereact/datatable";
import { InputSwitch, InputSwitchChangeEvent } from "primereact/inputswitch";
import { InputText } from "@/components/common";
import React, { useEffect, useState } from "react";
import { SubmitForm } from "../common";
import { Montserrat } from "next/font/google";
import Table from "../common/Table";
import { usePermissions } from "@/hooks/usePermissions";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { ERoles } from "@/domain/enums/ERoles";
const montserratFont = Montserrat({ subsets: ["latin"] });

export default function TeamsForm({
  team,
  setTeam,
  teamMembers,
  setTeamMembers,
  saveForm,
  cancelForm,
}: {
  team: ITeam | undefined;
  setTeam: React.Dispatch<React.SetStateAction<ITeam | undefined>>;
  teamMembers: TeamMemberDto[] | ITeamMember[] | undefined[] | undefined;
  setTeamMembers: React.Dispatch<
    React.SetStateAction<
      TeamMemberDto[] | ITeamMember[] | undefined[] | undefined
    >
  >;
  saveForm: () => void;
  cancelForm: () => void;
}) {
  const [users, setUsers] = useState<UserDto[] | undefined>([]);
  const [selectedUser, setSelectedUser] = useState<UserDto | undefined>(
    undefined
  );
  const [filteredUsers, setFilteredUsers] = useState<UserDto[] | undefined>(
    undefined
  );
  const { validatePermission, permissionConstants } = usePermissions();
  const user = useCurrentUser();

  const search = (event: AutoCompleteCompleteEvent) => {
    const _teamMembersIdList = teamMembers?.map((tm) => {
      return tm ? tm?.member?.id : undefined;
    });
    const _excludeCurrentTeamMembers = (member: UserDto) =>
      !_teamMembersIdList?.includes(member?.id ?? 0);
    const query = event.query.toLowerCase();

    let _filteredUsers: UserDto[] | undefined;
    _filteredUsers = !event.query.trim().length
      ? [...(users ?? [])]
      : users?.filter((user: UserDto) =>
          user.fullName?.toLowerCase().includes(query)
        );
    _filteredUsers = _filteredUsers?.filter?.(_excludeCurrentTeamMembers);

    setFilteredUsers(_filteredUsers);
  };
  const onChangeHandler = (e: AutoCompleteChangeEvent) =>
    setSelectedUser(e.value);
  const onSelectHandler = (e: AutoCompleteSelectEvent) => {
    const id = 0;
    const teamId = team?.id ?? 0;
    const member = e.value;
    const memberId = member.id;
    const isLead = member.roleId === ERoles.Lead;
    const maxCases = 5;
    const newMember = {
      id,
      teamId,
      memberId,
      isLead,
      maxCases,
      member,
    } as TeamMemberDto;

    setTeamMembers([...(teamMembers as TeamMemberDto[]), newMember]);
    setSelectedUser(undefined);
  };
  const onChangeNameHandler = (event: React.ChangeEvent<HTMLInputElement>) =>
    setTeam({
      ...team,
      name: event?.currentTarget?.value,
    } as ITeam);

  const isLeadRenderer = (data: TeamMemberDto, options: ColumnBodyOptions) => {
    const onChangeHandler = (event: InputSwitchChangeEvent) => {
      const modifiedMember: TeamMemberDto = {
        ...data,
        isLead: event.value || data?.member?.roleId === ERoles.Lead,
      };
      const _teamMembers = teamMembers?.map?.((tm) =>
        tm?.member?.id == data.member?.id ? modifiedMember : tm
      ) as TeamMemberDto[];
      setTeamMembers(_teamMembers);
    };
    const id = `leadership-${data.memberId}-toggle`;
    return (
      <InputSwitch
        inputId={id}
        checked={data?.isLead || data?.member?.roleId === ERoles.Lead}
        disabled={data?.member?.roleId !== ERoles.Lead}
        onChange={onChangeHandler}
      />
    );
  };
  const maxCasesRenderer = (
    data: TeamMemberDto,
    options: ColumnBodyOptions
  ) => {
    const changeValue = (i: number) => {
      let { maxCases } = data;
      maxCases += i;
      if (maxCases < 0) maxCases = 0;

      const modifiedMember: TeamMemberDto = { ...data, maxCases };
      const _teamMembers = teamMembers?.map?.((tm) =>
        tm?.member?.id == data.member?.id ? modifiedMember : tm
      ) as TeamMemberDto[];
      setTeamMembers(_teamMembers);
    };

    const decreaseMaxCases = () => changeValue(-1);
    const increaseMaxCases = () => changeValue(+1);

    const decId = `decrease-${data.memberId}-case-btn`;
    const incId = `increase-${data.memberId}-case-btn`;
    const qntId = `qnt-${data.memberId}-case-label`;

    const ret = (
      <div className="flex items-center gap-2">
        <i
          id={decId}
          className="pi pi-chevron-down hover:cursor-pointer"
          onClick={decreaseMaxCases}></i>
        <div id={qntId}>{data.maxCases}</div>
        <i
          id={incId}
          className="pi pi-chevron-up hover:cursor-pointer"
          onClick={increaseMaxCases}></i>
      </div>
    );
    return ret;
  };

  const removeTeamMemberRenderer = (
    data: TeamMemberDto,
    options: ColumnBodyOptions
  ) => {
    const removeHandler = () => {
      const _teamMembers = teamMembers?.filter?.(
        (tm) => tm && tm.member?.id != data.member?.id && tm
      ) as TeamMemberDto[];
      setTeamMembers(_teamMembers);
    };
    const id = `remove-${data.memberId}-case-btn`;
    return (
      <i
        id={id}
        className="pi pi-trash hover:cursor-pointer bg-[--papirus-yellow] text-white p-2 rounded-full"
        onClick={() => removeHandler()}></i>
    );
  };

  useEffect(() => {
    getUsersWithFilters({
      lazyState: {
        sortField: "id",
        sortOrder: 1,
        filters: {
          roleId: { matchMode: "IsNotEqualTo", value: ERoles.SuperAdmin },
        },
      },
    }).then((response) => {
      response?.data ? setUsers(response?.data) : setUsers([]);
    });
    if (team?.id) getTeamMembersByTeamId(team?.id).then(setTeamMembers);
  }, [team?.id, setTeamMembers]);

  return (
    <form className="flex flex-col py-3 gap-5" id="teamForm" action={saveForm}>
      <InputText
        id="name"
        name={"name"}
        defaultValue={team?.name ?? ""}
        type="text"
        label={"Nombre del equipo"}
        placeholder="Escriba el nombre del equipo"
        className="w-full h-12"
        onChange={onChangeNameHandler}
        required
      />
      <span className="p-float-label relative">
        <AutoComplete
          inputId="add-teammember-autocomplete-input"
          name="add-teammember-autocomplete"
          type="text"
          field="fullName"
          value={selectedUser}
          suggestions={filteredUsers}
          completeMethod={search}
          onChange={onChangeHandler}
          onSelect={onSelectHandler}
          pt={{
            root: {
              className:
                montserratFont.className +
                ` text-sm border-2 h-12 placeholder-transparent focus:placeholder-[--papirus-grey] rounded-[4px] [&:focus+label]:left-4 w-full`,
            },
          }}
        />
        <label
          htmlFor="add-teammember-autocomplete"
          className={
            montserratFont.className +
            ` absolute h-fit top-[calc(50%-5px)] truncate left-4 text-base`
          }>
          Agregar usuarios
        </label>
      </span>

      <Table
        id="team-form-table"
        lazy={false}
        dataKey="member.id"
        value={teamMembers as DataTableValueArray}
        emptyMessage="Debe agregar miembros a su equipo"
        paginator={false}>
        <Column body={removeTeamMemberRenderer}></Column>
        <Column field="member.firstName" header="Nombre"></Column>
        <Column field="member.lastName" header="Apellido"></Column>
        <Column field="member.email" header="Email"></Column>
        <Column field="isLead" header="Es líder" body={isLeadRenderer}></Column>
        <Column
          field="maxCases"
          header="Casos"
          body={maxCasesRenderer}></Column>
      </Table>

      <div className="mt-5 flex justify-end">
        <SubmitForm
          secondaryId={"cancel"}
          primaryId={"save"}
          secondaryText={"Cancelar"}
          primaryText={"Guardar"}
          onClickSecondary={cancelForm}
          disablePrimary={
            !validatePermission(permissionConstants.teams.create)
          }
        />
      </div>
    </form>
  );
}

const pt: AutoCompletePassThroughOptions = {
  root: {
    style: {
      border: "1px solid red",
      width: "100%",
    },
  },
  input: {
    style: {
      border: "1px solid red",
      width: "100%",
    },
  },
};
