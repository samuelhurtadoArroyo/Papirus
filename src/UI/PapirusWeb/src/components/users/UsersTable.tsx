"use client";
import { getUsersWithFilters } from "@/actions/client-utilities/getUsersWithFilters";
import { updateUserStatus } from "@/actions/users/updateUserStatus";
import { InputSwitch } from "@/components/common";
import { RoleDto, UserDto } from "@/domain/entities/data-contracts";
import { textConstants } from "@/domain/globalization/es";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import useLazyTable from "@/hooks/useLazyTable";
import useToast from "@/hooks/useToast";
import { getTableFilterTemplate } from "@/services/getTableFilterTemplate";
import Image from "next/image";
import Link from "next/link";
import { Column } from "primereact/column";
import { Toast } from "primereact/toast";
import { useCallback, useEffect, useState } from "react";
import Table from "../common/Table";
import { useSearchParams } from "next/navigation";
import { usePermissions } from "@/hooks/usePermissions";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";

const UsersTable = ({ roles }: { roles: RoleDto[] }) => {
  const { toastRef, showToast } = useToast();
  const { validatePermission, permissionConstants } = usePermissions();
  const [users, setUsers] = useState<UserDto[] | undefined>();
  const { rows, headers } = textConstants.pages.users.table;
  const altText = textConstants.components.alt.icons;
  const statusText = rows.status;
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
    matchModeEqualsOptions,
  } = useLazyTable(["firstName", "lastName", "email", "roleId", "isActive"]);
  const searchParams = useSearchParams();
  const debouncedValue = searchParams.get("query") || "";

  const getUsersFromDB = useCallback(
    (debouncedValue: string, lazyState: ILazyTableState) => {
      setIsLoading(true);
      getUsersWithFilters({ lazyState, debouncedValue })
        .then((response) => {
          response?.data ? setUsers(response?.data) : setUsers([]);
          setTotalRecords(response?.totalCount);
        })
        .finally(() => {
          setIsLoading(false);
        });
    },
    [setIsLoading, setTotalRecords, setUsers]
  );

  useEffect(() => {
    getUsersFromDB(debouncedValue, lazyState);
  }, [debouncedValue, lazyState, getUsersFromDB]);

  const handleStatusCheckToggle = async (user: UserDto, newStatus: boolean) => {
    await updateUserStatus({ ...user, isActive: newStatus })
      .then((response) => {
        if (response.errors && response.message) {
          showToast(response);
          throw new Error(String(response?.message));
        }
        showToast(response);
        getUsersFromDB(debouncedValue, lazyState);
      })
      .catch((error) => {
        throw error;
      });
  };

  const roleIdBodyTemplate = (rowData: UserDto) => {
    return (
      <span>
        {rowData.roleId
          ? roles.find((role) => role.id === rowData.roleId)?.name
          : "-"}
      </span>
    );
  };

  const statusBodyTemplate = (rowData: UserDto) => {
    return (
      <InputSwitch
        id={`user-switch-${rowData.id}`}
        onToggle={async () =>
          handleStatusCheckToggle(rowData, !rowData.isActive).catch((error) => {
            throw error;
          })
        }
        defaultValue={!!rowData.isActive}
        trueLabel={statusText.active}
        falseLabel={statusText.inactive}
      />
    );
  };

  return (
    <div className="flex items-center justify-between bg-[--white] w-full rounded-lg shadow-md">
      <Toast ref={toastRef} />
      <Table
        id="users-data-table"
        value={users}
        loading={isLoading}
        first={lazyState.first}
        rows={lazyState.rows}
        onRowsPerPageChange={setRows}
        onPage={onPage}
        onSort={onSort}
        totalRecords={totalRecords}
        sortField={lazyState.sortField}
        sortOrder={lazyState.sortOrder}
        filters={lazyState.filters}
        onFilter={onFilter}>
        <Column
          field="firstName"
          header={headers.firstName}
          sortable
          {...getTableFilterTemplate({
            id: "firstName",
            placeholder: headers.firstName,
            label: headers.firstName,
            inputType: "text",
            filterVariant: "text",
            filterMatchModeOptions: matchModeTextOptions,
          })}
        />
        <Column
          field="lastName"
          header={headers.lastName}
          sortable
          {...getTableFilterTemplate({
            id: "lastName",
            placeholder: headers.lastName,
            label: headers.lastName,
            inputType: "text",
            filterVariant: "text",
            filterMatchModeOptions: matchModeTextOptions,
          })}
        />
        <Column
          field="email"
          header={headers.email}
          sortable
          {...getTableFilterTemplate({
            id: "email",
            placeholder: headers.email,
            label: headers.email,
            inputType: "email",
            filterVariant: "email",
            filterMatchModeOptions: matchModeTextOptions,
          })}
        />
        <Column
          field="roleId"
          header={headers.role}
          body={roleIdBodyTemplate}
          {...getTableFilterTemplate({
            id: "roleId",
            filterVariant: "dropdown",
            label: headers.role,
            placeholder: headers.role,
            dropdownOptions: roles.map((role) => ({
              label: role.name,
              value: role.id,
            })),
            filterMatchModeOptions: matchModeEqualsOptions,
          })}
        />
        <Column
          field="isActive"
          header={headers.status}
          dataType="boolean"
          body={statusBodyTemplate}
          {...getTableFilterTemplate({
            id: "isActive",
            filterVariant: "boolean",
            filterMatchModeOptions: matchModeEqualsOptions,
            label: headers.status,
          })}
        />
        <Column
          headerClassName="min-w-[60px] w-[8%]"
          rowEditor={true}
          bodyClassName="text-center"
          filter={false}
          body={(rowData) => {
            return (
              <Link
                id={`user-edit-${rowData.id}-a`}
                href={`/users/${rowData.id}/edit`}
                className="papirus-icon-button"
                {...getDisabledLinkProps(
                  !validatePermission(permissionConstants.users.edit)
                )}>
                <Image
                  src="/edit.svg"
                  alt={altText.edit}
                  width={16}
                  height={16}
                />
              </Link>
            );
          }}
        />
      </Table>
    </div>
  );
};

export default UsersTable;
