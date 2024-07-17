"use client";
import { createUser } from "@/actions/users/createUser";
import { RoleDto } from "@/domain/entities/data-contracts";
import { textConstants } from "@/domain/globalization/es";
import useToast from "@/hooks/useToast";
import Image from "next/image";
import { Toast } from "primereact/toast";
import { useEffect } from "react";
import { useFormState } from "react-dom";
import { InputText, Dropdown } from "../common";
import UserFormLayout from "./UserFormLayout";
import UserSubmit from "./UserSubmit";
import { usePermissions } from "@/hooks/usePermissions";

const UsersCreateForm = ({ roles }: { roles: RoleDto[] }) => {
  const initialState = {
    message: undefined,
    errors: undefined,
    toastTitle: undefined,
    toastType: undefined,
    resetFormKey: undefined,
  };
  const { validatePermission, permissionConstants } = usePermissions();
  const [state, dispatch] = useFormState(createUser, initialState);
  const { toastRef, showToast } = useToast();
  const usersFormText = textConstants.components.usersForm;
  const altText = textConstants.components.alt.icons.user;

  useEffect(() => {
    !!state?.toastType && showToast(state);
  }, [showToast, state]);

  return (
    <>
      <Toast ref={toastRef} />
      <UserFormLayout>
        <form
          key={state?.resetFormKey}
          id="user-create-form"
          className="grid grid-cols-1 md:grid-cols-[200px_1fr] grid-rows-1 w-full px-5 md:px-10 lg:px-[90px] py-[70px] gap-[30px]"
          action={dispatch}>
          <div className="col-auto rounded-full bg-[--icon-grey-background] size-[166px] p-11 flex items-center justify-center aspect-square justify-self-center">
            <Image
              src={"/create-user.svg"}
              alt={altText}
              width={78}
              height={78}
            />
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 grid-rows-[repeat(8,_auto)] md:grid-rows-[repeat(4,_auto)] gap-[30px]">
            <InputText
              name={"firstName"}
              type="text"
              label={usersFormText.form.firstName}
              required
              showRequiredIcon
              className="w-full"
              errorMessages={state?.errors?.firstName}
              disabled={!validatePermission(permissionConstants.users.create)}
            />
            <InputText
              name={"lastName"}
              type="text"
              label={usersFormText.form.lastName}
              required
              showRequiredIcon
              className="w-full"
              errorMessages={state?.errors?.lastName}
              disabled={!validatePermission(permissionConstants.users.create)}
            />
            <InputText
              name={"email"}
              type="text"
              label={usersFormText.form.email}
              required
              showRequiredIcon
              className="w-full"
              errorMessages={state?.errors?.email}
              disabled={!validatePermission(permissionConstants.users.create)}
            />
            <Dropdown
              id="roles-dropdown"
              name={"roleId"}
              label={usersFormText.form.role}
              placeholder={usersFormText.form.role}
              showRequiredIcon
              className="w-full"
              errorMessages={state?.errors?.roleId}
              options={
                roles?.map((role) => {
                  if (role.name && role.id) {
                    return { label: role.name, value: role.id };
                  }
                }) as IDropdown[]
              }
              disabled={!validatePermission(permissionConstants.users.create)}
            />
            <InputText
              name={"password"}
              type="password"
              label={usersFormText.form.password}
              required
              showRequiredIcon
              className="w-full"
              errorMessages={state?.errors?.password}
              disabled={!validatePermission(permissionConstants.users.create)}
            />
            <InputText
              name={"confirmPassword"}
              type="password"
              label={usersFormText.form.confirmPassword}
              required
              showRequiredIcon
              className="w-full"
              errorMessages={state?.errors?.confirmPassword}
              disabled={!validatePermission(permissionConstants.users.create)}
            />
            <small className="align-middle items-center h-full flex text-xs gap-1">
              <span className="text-[--error]">*</span>
              <span>{usersFormText.form.guide}</span>
            </small>
            <UserSubmit />
          </div>
        </form>
      </UserFormLayout>
    </>
  );
};

export default UsersCreateForm;
