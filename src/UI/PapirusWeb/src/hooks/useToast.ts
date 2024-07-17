"use client";
import { TOAST_CONSTANTS } from "@/domain/constants/components";
import { Toast, ToastMessage } from "primereact/toast";
import { useCallback, useRef } from "react";

const useToast = () => {
  const toastRef = useRef<Toast>(null);

  const showToast = useCallback(
    ({
      toastType,
      toastTitle,
      message,
    }: {
      message?: ToastMessage["detail"];
      toastTitle?: ToastMessage["summary"];
      toastType?: ToastMessage["severity"];
    }) => {
      toastRef?.current?.show({
        severity: toastType,
        summary: toastTitle,
        detail: message,
        life: TOAST_CONSTANTS.duration,
      });
    },
    []
  );

  return { toastRef, showToast };
};

export default useToast;
