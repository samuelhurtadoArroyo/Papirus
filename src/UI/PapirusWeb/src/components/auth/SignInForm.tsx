"use client";
import { login } from "@/actions/auth/login";
import { textConstants } from "@/domain/globalization/es";
import useToast from "@/hooks/useToast";
import Image from "next/image";
import { useEffect, useState, FormEvent, useTransition } from "react";
import profilePic from "../../../public/full-logo.svg";
import { InputText } from "../common";
import SubmitAuthForm from "./SubmitAuthForm";
import { Toast } from "primereact/toast";
import { IAuthState } from "@/domain/interfaces/IFormState";

export default function SignInForm() {
  const initialState = {
    message: undefined,
    errors: undefined,
    toastTitle: undefined,
    toastType: undefined,
    resetFormKey: undefined,
  };
  const [isDisabled, setIsDisabled] = useState(true);
  const [state, setState] = useState<IAuthState>(initialState);
  const [isPending, startTransition] = useTransition();
  const { toastRef, showToast } = useToast();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const signInText = textConstants.components.auth.signin;
  const altLogoText = textConstants.components.alt.logo;

  const resetFields = () => {
    setEmail("");
    setPassword("");
  };

  const onSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    await startTransition(() => {
      login(new FormData(event.currentTarget)).then((data) => {
        setState(data);
        if (data?.errors) {
          resetFields();
        }
      });
    });
  };

  useEffect(() => {
    !!state?.toastType && showToast(state);
  }, [showToast, state]);

  useEffect(() => {
    if (!email || !password) {
      setIsDisabled(true);
    } else {
      setIsDisabled(false);
    }
  }, [email, password]);

  return (
    <>
      <Toast ref={toastRef} />
      <div className="text-center bg-white mx-auto rounded-xl flex flex-col gap-6 px-8  sm:px-20 md:px-24 py-4 md:py-7 w-full max-w-xl items-center justify-center">
        <Image src={profilePic} alt={altLogoText} height={98} width={130} />
        <section className="flex flex-col gap-1 justify-center items-center">
          <h1 id="welcome-h1" className="font-semibold text-2xl">
            {signInText.welcome}
          </h1>
          <p
            id="subtitle-p"
            className="font-medium text-sm text-[--text-secondary]">
            {signInText.subtitle}
          </p>
        </section>
        <form
          key={state?.resetFormKey}
          id="frmLoginForm"
          onSubmit={onSubmit}
          className="flex flex-col gap-6 justify-center items-center w-full">
          <InputText
            type="email"
            name="email"
            required
            value={email}
            setValue={setEmail}
            label={signInText.email}
            containerClassName="w-full"
          />
          <InputText
            type="password"
            name="password"
            required
            value={password}
            setValue={setPassword}
            label={signInText.password}
            containerClassName="w-full"
          />
          <SubmitAuthForm
            id={"login-btn"}
            text={signInText.logIn}
            disable={isDisabled || isPending}
          />
        </form>
        {isPending && <i className="pi pi-spin pi-spinner"></i>}
      </div>
    </>
  );
}
