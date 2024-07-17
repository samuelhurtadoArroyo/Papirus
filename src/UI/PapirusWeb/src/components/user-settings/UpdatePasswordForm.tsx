"use client";
import { textConstants } from "@/domain/globalization/es";
import useToast from "@/hooks/useToast";
import { Toast } from "primereact/toast";
import React, { useEffect } from "react";
import { useFormState } from "react-dom";
import { InputText } from "../common";
import { updatePassword } from "@/actions/users/updatePassword";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import SubmitForm from "../common/SubmitForm";
import { useRouter } from "next/navigation";

const UpdatePasswordForm = () => {
  const initialState = {
    message: undefined,
    errors: undefined,
    toastTitle: undefined,
    toastType: undefined,
    resetPasswordKey: undefined,
  };
  const [state, dispatch] = useFormState(updatePassword, initialState);
  const currentUser = useCurrentUser();
  const router = useRouter();
  const { toastRef, showToast } = useToast();
  const updatePasswordText = textConstants.pages.updatePassword;

  useEffect(() => {
    !!state?.toastType && showToast(state);
  }, [showToast, state]);

  return (
    <div className="flex flex-col items-center justify-between bg-[--white] w-full rounded-lg shadow-md">
      <Toast ref={toastRef} />
      <form
        key={state?.resetFormKey}
        id="update-password-loggued-form"
        className="flex flex-col w-full md:max-w-96 px-5 py-[70px] gap-[30px]"
        action={dispatch}>
        {currentUser?.email ? (
          <input
            id="hidden-email"
            name={"email"}
            type="text"
            defaultValue={currentUser?.email}
            hidden
          />
        ) : null}
        <InputText
          name={"currentPassword"}
          type="password"
          label={updatePasswordText.form.currentPassword}
          required
          className="w-full"
          errorMessages={state?.errors?.currentPassword}
        />
        <InputText
          name={"newPassword"}
          type="password"
          label={updatePasswordText.form.newPassword}
          required
          className="w-full"
          errorMessages={state?.errors?.newPassword}
        />
        <InputText
          name={"confirmPassword"}
          type="password"
          label={updatePasswordText.form.confirmPassword}
          required
          className="w-full"
          errorMessages={state?.errors?.confirmPassword}
        />
        <SubmitForm
          secondaryId={"cancel-btn"}
          primaryId={"save-btn"}
          secondaryText={updatePasswordText.form.cancel}
          onClickSecondary={() => router.back()}
          primaryText={updatePasswordText.form.save}
        />
      </form>
    </div>
  );
};

export default UpdatePasswordForm;
