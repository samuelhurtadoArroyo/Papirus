"use client";
import { Montserrat } from "next/font/google";
import { Card } from "primereact/card";
import React, { useCallback, useEffect, useRef, useState } from "react";
import { Button } from "../common";
import { useRouter } from "next/navigation";
import { textConstants } from "@/domain/globalization/es";
import { usePermissions } from "@/hooks/usePermissions";
import {
  CaseDto,
  CaseDocumentFieldValueDto,
} from "@/domain/entities/data-contracts";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import { TOAST_CONSTANTS } from "@/domain/constants/components";
import { updateCaseDocumentFieldValues } from "@/actions/guardianships/autoadmit/updateCaseDocumentFieldValues";
import { Toast } from "primereact/toast";
import usePendingState from "@/hooks/usePendingState";

const defaultFont = Montserrat({ subsets: ["latin"] });

export default function EmailContainer({
  caseData,
  extractedData,
}: {
  caseData: CaseDto | undefined;
  extractedData?: CaseDocumentFieldValueDto[];
}) {
  const emailText = textConstants.pages.email;
  const { validatePermission, permissionConstants } = usePermissions();
  const [fields, setFields] = useState<CaseDocumentFieldValueDto[] | undefined>(
    extractedData
  );
  const { isPending, setIsPending } = usePendingState();
  const router = useRouter();
  const [redirect, setRedirect] = useState(false);
  const [fieldsChanged, setFieldsChanged] = useState<boolean>(false);
  const toast = useRef<Toast>(null);
  const continueCallback = useCallback(() => {
    router.refresh();
    router.push(
      `/guardianships/${caseData?.id}/extracted-data/resume${
        "?u=" + String(new Date().getTime())
      }`
    );
  }, [caseData?.id, router]);

  useEffect(() => {
    let timeout: NodeJS.Timeout | undefined;
    if (redirect) {
      timeout = setTimeout(() => {
        continueCallback();
      }, TOAST_CONSTANTS.duration / 2);
    }
    return () => {
      clearTimeout(timeout);
    };
  }, [continueCallback, redirect]);

  const fieldSet = (field: CaseDocumentFieldValueDto) => {
    const { id, name, fieldValue } = field;

    return (
      <tr key={id}>
        <td className="w-[40%]">
          <label htmlFor={id?.toString()}>{name}</label>
        </td>
        <td className="w-[60%]">
          <input
            id={id?.toString()}
            name={name?.toString()}
            defaultValue={fieldValue?.toString()}
            onChange={fieldValueChangeHandler}
            disabled={
              caseData?.isAnswered ||
              !validatePermission(permissionConstants.extractedData.edit)
            }
          />
        </td>
      </tr>
    );
  };

  const fieldValueChangeHandler = (e: any) => {
    const { id, value: fieldValue } = e.target;
    setRedirect(false);
    setFields(
      fields?.map((field) =>
        field?.id == id ? { ...field, fieldValue } : field
      )
    );
    setFieldsChanged(true);
  };

  const formSubmitHandler = (e: any) => {
    e.preventDefault();
    setIsPending(true);
    let severity = "success" as
      | "error"
      | "success"
      | "info"
      | "warn"
      | undefined;
    let summary = emailText.title;
    let detail = emailText.onFieldsUpdates.successMessage;
    let life = 3000;

    try {
      const updateData = async () => {
        if (fieldsChanged) {
          await updateCaseDocumentFieldValues(
            fields as CaseDocumentFieldValueDto[]
          )
            .then(console.debug)
            .catch((error) => {
              severity = "error";
              detail =
                emailText.onFieldsUpdates.errorMessage + error?.toString();
              toast.current?.show({ severity, summary, detail, life });
              throw new Error(detail);
            });
        }

        setFieldsChanged(false);
        toast.current?.show({ severity, summary, detail, life });
        setRedirect(true);
        setIsPending(false);
      };
      updateData();
    } catch (error) {
      severity = "error";
      detail = emailText.onFieldsUpdates.errorMessage + error?.toString();
      toast.current?.show({ severity, summary, detail, life });
      setIsPending(false);
    }
  };

  return (
    <form onSubmit={formSubmitHandler} className="w-full">
      <Toast ref={toast} />
      <Card
        className={defaultFont.className + " w-full"}
        title={emailText.table.title}>
        <h1 className="font-semibold text-xl">{emailText.title}</h1>
        <h2 className="font-normal text-lg">Email</h2>
        <div className="grid w-full gap-4 grid-cols-1 lg:grid-cols-2">
          <Card className={defaultFont.className}>
            <div
              className="overflow-y-auto w-full aspect-video lg:aspect-[70/99]"
              dangerouslySetInnerHTML={{
                __html: caseData?.emailHtmlBody ?? "<span>No data</span>",
              }}></div>
          </Card>

          <Card
            className={defaultFont.className + " h-fit"}
            title={emailText.table.subtitle}>
            <table className="data-table">
              <thead className="border-b border-b-[--table-border] text-left">
                <tr>
                  <th>{emailText.table.headers.field}</th>
                  <th>{emailText.table.headers.value}</th>
                </tr>
              </thead>
              <tbody>{fields?.map(fieldSet)}</tbody>
            </table>
          </Card>
        </div>
      </Card>

      <div className="flex gap-2 justify-center my-5 h-10">
        <Button
          type="button"
          className="uppercase"
          id="cancel-btn"
          variant="secondary"
          label={emailText.bottomButtons.cancel}
          onClick={() => {
            router.push(
              `/guardianships/${caseData?.id}/extracted-data/${
                EGuardianshipDocumentTypes.Autoadmit
              }${"?u=" + String(new Date().getTime())}`
            );
          }}
          disabled={!validatePermission(permissionConstants.extractedData.view)}
        />
        {!fieldsChanged ? (
          <Button
            type="button"
            className="uppercase"
            id="save-btn"
            variant="primary"
            label={emailText.bottomButtons.continue}
            onClick={() => {
              router.push(
                `/guardianships/${caseData?.id}/extracted-data/resume${
                  "?u=" + String(new Date().getTime())
                }`
              );
            }}
            disabled={
              isPending ||
              !validatePermission(permissionConstants.extractedData.view)
            }
          />
        ) : (
          <Button
            type="submit"
            className="uppercase"
            id="save-btn"
            variant="primary"
            label={emailText.bottomButtons.save}
            disabled={
              isPending ||
              !fieldsChanged ||
              !validatePermission(permissionConstants.extractedData.view)
            }
          />
        )}
      </div>
    </form>
  );
}
