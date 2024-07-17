import { PAGINATION_CONSTANTS } from "@/domain/constants/components";
import { textConstants } from "@/domain/globalization/es";
import { Dropdown } from "primereact/dropdown";
import {
  PaginatorCurrentPageReportOptions,
  PaginatorFirstPageLinkOptions,
  PaginatorNextPageLinkOptions,
  PaginatorPrevPageLinkOptions,
  PaginatorRowsPerPageDropdownOptions,
  PaginatorTemplateOptions,
} from "primereact/paginator";
import { classNames } from "primereact/utils";
import { Ripple } from "primereact/ripple";
import Image from "next/image";
import { Montserrat } from "next/font/google";
const montserratFont = Montserrat({ subsets: ["latin"] });

export const getPaginatorTemplate = ({
  onRowsPerPageChange,
  rows,
}: {
  onRowsPerPageChange: (row: number) => void;
  rows?: number;
}): PaginatorTemplateOptions => {
  const rowsPerPageOptions = PAGINATION_CONSTANTS.rowsPerPage;
  const { pagination: paginationText, alt: altText } = textConstants.components;

  return {
    layout: "RowsPerPageDropdown CurrentPageReport FirstPageLink PrevPageLink NextPageLink LastPageLink",
    RowsPerPageDropdown: (options: PaginatorRowsPerPageDropdownOptions) => {
      const dropdownOptions = rowsPerPageOptions.map((pages) => {
        return { label: pages, value: pages };
      });

      return (
        <>
          <span className="mx-1 text-xs font-semibold">
            {paginationText.elementsPerPage}:
          </span>
          <Dropdown
            className="text-xs border-b-[--black-42] border-b-2 rounded-none aspect-square h-auto"
            id="rows-per-page-dropdown"
            value={rows || options.value}
            options={dropdownOptions}
            itemTemplate={(option) => (
              <span id={`option-${option.value}`}>{option.value}</span>
            )}
            onChange={(event) => onRowsPerPageChange(event.target.value)}
            dropdownIcon={
              <Image
                src={"/down-icon.svg"}
                alt={altText.icons.down}
                width={12}
                height={6}
              />
            }
            pt={{
              input: {
                className:
                  montserratFont.className +
                  " flex p-0 items-center pl-2 text-xs",
              },
              wrapper: {
                className: montserratFont.className + " text-xs",
              },
              trigger: {
                className: "px-2 w-auto",
              },
            }}
          />
        </>
      );
    },
    CurrentPageReport: (options: PaginatorCurrentPageReportOptions) => {
      return (
        <span className="text-center w-[10%] min-w-16">
          {options.first} - {options.last} {paginationText.of}{" "}
          {options.totalRecords}
        </span>
      );
    },
    FirstPageLink: (options) => {
      return (
        <button
          id="first-btn"
          type="button"
          className={classNames(options.className, "border-round")}
          onClick={options.onClick}
          disabled={options.disabled}>
          <i className="pi pi-angle-double-left"/>
          <Ripple />
        </button>
      );
    },
    PrevPageLink: (options: PaginatorPrevPageLinkOptions) => {
      return (
        <button
          id="previous-btn"
          type="button"
          className={classNames(options.className, "border-round")}
          onClick={options.onClick}
          disabled={options.disabled}>
          <Image
            src={"/previous-icon.svg"}
            alt={altText.icons.previous}
            width={8}
            height={12}
          />
          <Ripple />
        </button>
      );
    },
    NextPageLink: (options: PaginatorNextPageLinkOptions) => {
      return (
        <button
          id="next-btn"
          type="button"
          className={classNames(options.className, "border-round")}
          onClick={options.onClick}
          disabled={options.disabled}>
          <Image
            src={"/next-icon.svg"}
            alt={altText.icons.next}
            width={8}
            height={12}
          />
          <Ripple />
        </button>
      );
    },
    LastPageLink(options) {
      return (
        <button
          id="last-btn"
          type="button"
          className={classNames(options.className, "border-round")}
          onClick={options.onClick}
          disabled={options.disabled}>
          <i className="pi pi-angle-double-right" />
          <Ripple />
        </button>
      );
    },
  };
};
