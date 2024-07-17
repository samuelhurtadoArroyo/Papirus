"use client";
import { Dropdown, DropdownProps } from "primereact/dropdown";
import { Montserrat } from "next/font/google";
const montserratFont = Montserrat({ subsets: ["latin"] });
import Image from "next/image";
import { textConstants } from "@/domain/globalization/es";

const TableRowDropdown = ({
  id,
  value,
  options,
  onChange,
  placeholder,
  itemTemplate,
  optionLabel,
  ...props
}: DropdownProps) => {
  const altText = textConstants.components.alt.icons;

  return (
    <Dropdown
      id={id}
      value={value || undefined}
      options={options}
      onChange={onChange}
      placeholder={placeholder}
      className="border-2"
      optionLabel={optionLabel}
      dropdownIcon={
        <Image
          src={"/down-icon.svg"}
          alt={altText.down}
          width={12}
          height={6}
        />
      }
      itemTemplate={itemTemplate}
      pt={{
        item: { className: montserratFont.className + " py-4" },
        root: { className: montserratFont.className + " h-[28px]" },
        input: {
          className:
            montserratFont.className +
            ` text-xs pl-2 pr-4 py-0 flex items-center ${
              placeholder ? "uppercase text-[--papirus-grey] uppercase" : ""
            }`,
        },
        trigger: {
          className: "px-1 w-auto",
        },
      }}
      {...props}
    />
  );
};

export default TableRowDropdown;
