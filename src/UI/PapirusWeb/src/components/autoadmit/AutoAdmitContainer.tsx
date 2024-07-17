"use client";
import { Montserrat } from "next/font/google";
import { Card } from "primereact/card";
import { useCallback, useEffect, useRef, useState } from "react";
import { Button, Dropdown } from "../common";
import { useRouter } from "next/navigation";
import { textConstants } from "@/domain/globalization/es";
import { usePermissions } from "@/hooks/usePermissions";
import {
  BusinessLineDto,
  CaseDocumentFieldValueDto,
  CaseDto,
} from "@/domain/entities/data-contracts";
import { DropdownChangeEvent } from "primereact/dropdown";
import { Toast } from "primereact/toast";
import { updateCase } from "@/actions/cases/updateCase";
import { updateCaseDocumentFieldValues } from "@/actions/guardianships/autoadmit/updateCaseDocumentFieldValues";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import { TOAST_CONSTANTS } from "../../domain/constants/components";
import { ICase } from "@/domain/interfaces/ICase";
import ViewDocument from "../documents/ViewDocument";
import usePendingState from "@/hooks/usePendingState";

const defaultFont = Montserrat({ subsets: ["latin"] });

export default function AutoAdmitContainer({
  caseData,
  lines,
  fieldsData,
}: {
  caseData?: ICase;
  lines: BusinessLineDto[] | undefined;
  fieldsData?: CaseDocumentFieldValueDto[];
}) {
  const autoadmitText = textConstants.pages.autoadmit;
  const { validatePermission, permissionConstants } = usePermissions();
  const [guardianship, setGuardianship] = useState<ICase | undefined>(caseData);
  const [fields, setFields] = useState<CaseDocumentFieldValueDto[] | undefined>(
    fieldsData
  );
  const [guardianshipChanged, setGuardianshipChanged] =
    useState<boolean>(false);
  const [fieldsChanged, setFieldsChanged] = useState<boolean>(false);
  const [businessLine, setBusinessLine] = useState<BusinessLineDto>();
  const { isPending, setIsPending } = usePendingState();
  const router = useRouter();
  const toast = useRef<Toast>(null);
  const enableContinue =
    guardianship?.businessLineId && !guardianshipChanged && !fieldsChanged;
  const disableSaveButton =
    (!guardianship?.businessLineId && !guardianshipChanged) ||
    (!guardianshipChanged && !fieldsChanged) ||
    !validatePermission(permissionConstants.extractedData.edit);

  const businessLineInitialValue = useCallback(() => {
    if (guardianship?.businessLineId && !businessLine) {
      setBusinessLine(
        lines?.filter((val) => val.id == guardianship?.businessLineId)?.[0]
      );
    }
  }, [businessLine, guardianship, lines]);
  const [redirect, setRedirect] = useState(false);
  useEffect(businessLineInitialValue, [businessLineInitialValue]);

  const continueCallback = useCallback(() => {
    router.push(
      `/guardianships/${caseData?.id}/extracted-data/${
        EGuardianshipDocumentTypes.Email
      }${"?u=" + String(new Date().getTime())}`
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

  const dropdownChangeHandler = (e: DropdownChangeEvent) => {
    setRedirect(false);
    setGuardianship({
      ...guardianship,
      businessLineId: Number.parseInt(e.value),
    } as CaseDto);
    setGuardianshipChanged(true);
  };

  const dropdownOptions = lines?.map(businessLine2iDropDown);

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

  const formSubmitHandler = async (e: any) => {
    e.preventDefault();
    setIsPending(true);
    let severity = "success" as
      | "error"
      | "success"
      | "info"
      | "warn"
      | undefined;
    let summary = autoadmitText.title;
    let detail = autoadmitText.onFieldsUpdates.successMessage;
    let life = 3000;

    try {
      const updateData = async () => {
        if (guardianshipChanged) {
          await updateCase(guardianship as CaseDto)
            .then((updatedGuardianship) => {
              setGuardianship({
                ...(updatedGuardianship as ICase),
              });
            })
            .catch((error) => {
              severity = "error";
              detail =
                autoadmitText.onFieldsUpdates.errorMessage + error?.toString();
              toast.current?.show({ severity, summary, detail, life });
              throw new Error(detail);
            });
        }

        if (fieldsChanged) {
          await updateCaseDocumentFieldValues(
            fields as CaseDocumentFieldValueDto[]
          )
            .then(console.debug)
            .catch((error) => {
              severity = "error";
              detail =
                autoadmitText.onFieldsUpdates.errorMessage + error?.toString();
              toast.current?.show({ severity, summary, detail, life });
              throw new Error(detail);
            });
        }

        setGuardianshipChanged(false);
        setFieldsChanged(false);
        toast.current?.show({ severity, summary, detail, life });
        setRedirect(true);
        setIsPending(false);
      };
      updateData();
    } catch (error) {
      severity = "error";
      detail = autoadmitText.onFieldsUpdates.errorMessage + error?.toString();
      toast.current?.show({ severity, summary, detail, life });
      setIsPending(false);
    }
  };

  return (
    <form onSubmit={formSubmitHandler} className="w-full">
      <Toast ref={toast} />
      <Card
        className={defaultFont.className + " w-full"}
        title={autoadmitText.table.title}>
        <h1 className="font-semibold text-xl">{autoadmitText.title}</h1>
        <h2 className="font-normal text-lg">{guardianship?.fileName}</h2>
        <div className="grid w-full gap-4 grid-cols-1 lg:grid-cols-2">
          <ViewDocument
            documentPath={guardianship?.filePath || ""}
            documentName={guardianship?.fileName || ""}
            className="w-full border aspect-video lg:aspect-[70/99] rounded-lg"
          />

          <Card
            className={defaultFont.className + " h-fit"}
            title={autoadmitText.table.subtitle}>
            {fields && lines ? (
              <table className="data-table">
                <thead className="border-b border-b-[--table-border] text-left">
                  <tr>
                    <th>{autoadmitText.table.headers.field}</th>
                    <th>{autoadmitText.table.headers.value}</th>
                  </tr>
                </thead>
                <tbody>
                  {fields?.map(fieldSet)}
                  <tr>
                    <td className="w-[40%]">
                      <label htmlFor="businessLine">
                        {autoadmitText.businessLine}
                      </label>
                    </td>
                    <td className="w-[60%]">
                      <Dropdown
                        id="businessLine"
                        value={guardianship?.businessLineId}
                        options={dropdownOptions}
                        name="businessLine"
                        label={autoadmitText.businessLine}
                        onChange={dropdownChangeHandler}
                        disabled={
                          caseData?.isAnswered ||
                          !validatePermission(
                            permissionConstants.extractedData.edit
                          )
                        }
                      />
                    </td>
                  </tr>
                </tbody>
              </table>
            ) : (
              <div className="text-center py-10">
                <i
                  className="pi pi-spin pi-spinner"
                  style={{ fontSize: "2rem" }}></i>
              </div>
            )}
          </Card>
        </div>
      </Card>

      {fields && lines && (
        <div className="flex gap-2 justify-center my-5 h-10">
          <Button
            type="button"
            className="uppercase"
            id="cancel-btn"
            variant="secondary"
            label={autoadmitText.bottomButtons.cancel}
            onClick={() =>
              router.push(`/guardianships/${caseData?.id}/documents`)
            }
            disabled={
              !validatePermission(permissionConstants.extractedData.view)
            }
          />
          {enableContinue ? (
            <Button
              type="button"
              className="uppercase"
              id="save-btn"
              variant="primary"
              label={autoadmitText.bottomButtons.continue}
              onClick={() => {
                router.push(
                  `/guardianships/${caseData?.id}/extracted-data/${
                    EGuardianshipDocumentTypes.Email
                  }${"?u=" + String(new Date().getTime())}`
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
              label={autoadmitText.bottomButtons.save}
              disabled={isPending || disableSaveButton}
            />
          )}
        </div>
      )}
    </form>
  );
}

const businessLine2iDropDown = (businessLine: BusinessLineDto) =>
  ({
    label: businessLine.name,
    value: businessLine.id,
    payload: businessLine,
  } as IDropdown);
