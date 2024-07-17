import { Button, Dropdown, InputText } from "@/components/common";
import { textConstants } from "@/domain/globalization/es";
import { Montserrat } from "next/font/google";
import {
  ColumnFilterApplyTemplateOptions,
  ColumnFilterClearTemplateOptions,
  ColumnFilterElementTemplateOptions,
  ColumnFilterMatchModeOptions,
  ColumnPassThroughOptions,
} from "primereact/column";
import { DropdownChangeEvent } from "primereact/dropdown";
import {
  TriStateCheckbox,
  TriStateCheckboxChangeEvent,
} from "primereact/tristatecheckbox";
import { HTMLInputTypeAttribute } from "react";

const montserratFont = Montserrat({ subsets: ["latin"] });

export const getTableFilterTemplate = ({
  id,
  inputType,
  filterVariant,
  dropdownOptions,
  filterMatchModeOptions,
  placeholder = "",
  label = "",
}: {
  id: string;
  inputType?: HTMLInputTypeAttribute;
  filterVariant: "text" | "email" | "boolean" | "date" | "number" | "dropdown";
  dropdownOptions?: IDropdown[];
  filterMatchModeOptions: ColumnFilterMatchModeOptions[];
  placeholder?: string;
  label?: string;
}) => {
  const { clear, apply } = textConstants.filters;

  return {
    id: id,
    filter: true,
    filterType: inputType,
    filterMatchModeOptions: filterMatchModeOptions,
    showApplyButton: false,
    filterElement: (options: ColumnFilterElementTemplateOptions) => {
      if (filterVariant === "boolean") {
        return (
          <div className="flex gap-3 items-center w-full">
            <TriStateCheckbox
              value={options.value}
              onChange={(e: TriStateCheckboxChangeEvent) =>
                options.filterApplyCallback(e.value)
              }
              pt={{
                box: { className: "border-2 border-[--papirus-purple]" },
                input: {
                  id: `${id}-filter-checkbox`,
                  name: `${id}-filter-checkbox`,
                },
              }}
              variant="filled"
            />
            <label
              htmlFor={`${id}-filter-checkbox`}
              className={montserratFont.className + " text-sm"}>
              {label}
            </label>
          </div>
        );
      } else if (filterVariant === "dropdown") {
        return (
          <Dropdown
            id={`${id}-filter-dropdown`}
            value={options.value || undefined}
            options={dropdownOptions}
            onChange={(e: DropdownChangeEvent) =>
              options.filterApplyCallback(e.value)
            }
            className="p-column-filter w-full"
            name={`${id}-filter-dropdown`}
            label={label}
            placeholder={placeholder}
          />
        );
      } else {
        return (
          <InputText
            id={`${id}-filter-input`}
            name={`${id}-filter-input`}
            label={label}
            placeholder={placeholder}
            type={inputType || "text"}
            value={options.value}
            onChange={(e) => options.filterApplyCallback(e.target.value)}
          />
        );
      }
    },
    filterClear: (options: ColumnFilterClearTemplateOptions) => {
      return (
        <Button
          label={clear}
          id={"clear-btn"}
          variant={"secondary"}
          disabled={options.filterModel?.value === null}
          onClick={() => options.filterClearCallback()}
        />
      );
    },
    filterApply: (options: ColumnFilterApplyTemplateOptions) => (
      <Button
        label={apply}
        id={"apply-btn"}
        variant={"primary"}
        disabled={options.filterModel?.value === null}
        onClick={() => options.filterApplyCallback()}
      />
    ),
    pt: {
      filterMatchModeDropdown: {
        root: {
          className:
            montserratFont.className +
            " text-sm h-10 border-2 rounded-[4px] pl-5 py-0 w-full",
          id: `${id}-filter-match-mode-dropdown`,
        },
        input: {
          className:
            montserratFont.className + " text-sm p-0 flex items-center",
        },
        item: { className: montserratFont.className + " text-sm" },
      },
      filterConstraint: {
        className: montserratFont.className + " text-sm flex flex-col gap-3",
      },
      filterMenuButton: (options) => {
        return {
          className: options?.context.active ? "bg-[--papirus-purple-50]" : "",
        };
      },
      filterRemove: {
        className: "hidden",
      },
      sort: {
        id: `${id}-sort`,
      },
    } as ColumnPassThroughOptions,
  };
};
