"use client";
import { updatePassword } from "@/actions/auth/updateMustChangePassword";
import { textConstants } from "@/domain/globalization/es";
import useToast from "@/hooks/useToast";
import Image from "next/image";
import { Toast } from "primereact/toast";
import { useFormState } from "react-dom";
import profilePic from "../../../public/full-logo.svg";
import { InputText } from "../common";
import SubmitAuthForm from "./SubmitAuthForm";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { signOut } from "next-auth/react";
import { useEffect } from "react";
import { Button } from "primereact/button";

export default function MustChangePasswordForm() {
  const initialState = {
    message: undefined,
    errors: undefined,
    toastTitle: undefined,
    toastType: undefined,
    resetFormKey: undefined,
  };
  const [state, dispatch] = useFormState(updatePassword, initialState);
  const currentUser = useCurrentUser();
  const { toastRef, showToast } = useToast();

  const updatePasswordText = textConstants.pages.updatePassword;
  const altLogoText = textConstants.components.alt.logo;

  useEffect(() => {
    !!state?.toastType && showToast(state);
  }, [showToast, state]);

  useEffect(() => {
    const timer = setTimeout(() => {
      state?.redirect && signOut();
    }, 3000);
    return () => clearTimeout(timer);
  }, [state?.redirect]);

  const onLogout = () => {
    signOut();
  };

  return (
    <>
      <Toast ref={toastRef} />
      <div className="text-center bg-white mx-auto rounded-xl flex flex-col gap-6 px-8  sm:px-20 md:px-24 py-4 md:py-7 w-full max-w-xl items-center justify-center">
        <Image src={profilePic} alt={altLogoText} height={98} width={130} />
        <section className="flex flex-col gap-1 justify-center items-center">
          <h1 id="welcome-h1" className="font-semibold text-2xl">
            {updatePasswordText.header.title}
          </h1>
        </section>
        <form
          key={state?.resetFormKey}
          id="frmLoginForm"
          action={dispatch}
          className="flex flex-col gap-6 justify-center items-center w-full">
          {currentUser?.email ? (
            <input
              name={"email"}
              type="text"
              defaultValue={currentUser?.email}
              hidden
            />
          ) : null}

          <InputText
            name={"email"}
            type="email"
            label={updatePasswordText.form.email}
            required
            containerClassName="w-full"
            defaultValue={currentUser?.email}
            errorMessages={state?.errors?.email}
            disabled
          />
          <InputText
            name={"currentPassword"}
            type="password"
            label={updatePasswordText.form.currentPassword}
            required
            containerClassName="w-full"
            errorMessages={state?.errors?.currentPassword}
          />
          <InputText
            name={"newPassword"}
            type="password"
            label={updatePasswordText.form.newPassword}
            required
            containerClassName="w-full"
            errorMessages={state?.errors?.newPassword}
          />
          <InputText
            name={"confirmPassword"}
            type="password"
            label={updatePasswordText.form.confirmPassword}
            required
            containerClassName="w-full"
            errorMessages={state?.errors?.confirmPassword}
          />
          <SubmitAuthForm
            id={"save-password-btn"}
            text={updatePasswordText.form.save}
          />
        </form>
        <Button
          id="cancel-btn"
          label={updatePasswordText.form.cancel}
          onClick={onLogout}
          className="text-[--link] text-xs"
        />
      </div>
    </>
  );
}
