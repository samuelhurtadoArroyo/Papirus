import { ToastMessage } from "primereact/toast";
import { CaseProcessDocumentDto } from "../entities/data-contracts";

export interface IAuthState {
  errors?: {
    email?: string[];
    password?: string[];
  };
  message?: ToastMessage["detail"];
  toastTitle?: ToastMessage["summary"];
  toastType?: ToastMessage["severity"];
  resetFormKey?: string;
}

export interface IUserState {
  errors?: {
    firstName?: string[];
    lastName?: string[];
    email?: string[];
    roleId?: string[];
    password?: string[];
    confirmPassword?: string[];
  };
  message?: ToastMessage["detail"];
  toastTitle?: ToastMessage["summary"];
  toastType?: ToastMessage["severity"];
  resetFormKey?: string;
  redirect?: boolean;
}

export interface IPasswordState {
  errors?: {
    email?: string[];
    currentPassword?: string[];
    newPassword?: string[];
    confirmPassword?: string[];
  };
  message?: ToastMessage["detail"];
  toastTitle?: ToastMessage["summary"];
  toastType?: ToastMessage["severity"];
  resetFormKey?: string;
  redirect?: boolean;
}

export interface IGuardianshipState {
  errors?: {
    id?: string[];
    currentStatus?: string[];
    nextStatus?: string[];
  };
  message?: ToastMessage["detail"];
  toastTitle?: ToastMessage["summary"];
  toastType?: ToastMessage["severity"];
  resetFormKey?: string;
  continue?: boolean;
}

export interface ITemplateState {
  errors?: {
    id?: string[];
    caseId?: string[];
    templateId?: string[];
  };
  message?: ToastMessage["detail"];
  toastTitle?: ToastMessage["summary"];
  toastType?: ToastMessage["severity"];
  resetFormKey?: string;
  data?: CaseProcessDocumentDto;
  continue?: boolean;
}
