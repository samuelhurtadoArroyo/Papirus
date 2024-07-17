"use client";
import React, { Dispatch, ReactNode, useEffect, useState } from "react";
import { DropdownChangeEvent, DropdownProps, Dropdown as PrimeDropdown } from "primereact/dropdown";
import { Montserrat } from "next/font/google";
const montserratFont = Montserrat({ subsets: ["latin"] });

const Dropdown = ({
  id,
  value,
  setValue,
  defaultValue,
  onChange,
  name,
  placeholder,
  iconRight,
  iconLeft,
  className,
  showRequiredIcon,
  errorMessages,
  options,
  containerClassName,
  label,
  ...props
}: {
  id?: string;
  value?: number | null | undefined;
  setValue?: Dispatch<string>;
  onChange?: (event: DropdownChangeEvent) => void;
  name: string;
  label: string;
  placeholder?: string;
  iconRight?: ReactNode;
  iconLeft?: ReactNode;
  className?: string;
  showRequiredIcon?: boolean;
  errorMessages?: string[];
  options?: IDropdown[];
  containerClassName?: string;
  defaultValue?: IDropdown["value"];
} & DropdownProps) => {
  const [currentSelected, setCurrentSelected] = useState<any>(defaultValue || value || null);
  const [errors, setErrors] = useState(errorMessages);

  useEffect(() => {
    errorMessages && setErrors(errorMessages);

    return () => {
      setErrors(undefined);
    };
  }, [errorMessages]);

  const getCurrentSelectedValue = () => {
    if (value === 0 || currentSelected === 0) {
      return 0;
    }
    return value || currentSelected || undefined;
  };

  return (
    <div className={`flex flex-col ${containerClassName ?? "w-auto"}`}>
      <span className={`p-float-label relative w-full ${!!iconRight ? "p-input-icon-right" : ""} ${!!iconLeft ? "p-input-icon-left" : ""}`}>
        {iconLeft ? <i className="absolute left-4 top-1/2 w-fit -mt-[0.5rem]">{iconLeft}</i> : null}
        <PrimeDropdown
          id={id ?? name}
          inputId={name}
          name={name}
          value={getCurrentSelectedValue()}
          className={`text-sm h-10 border-2 ${errors ? "border-[--error] shadow-none" : ""} rounded-[4px] pl-5 py-0 [&:focus+label]:left-4 ${!!iconLeft ? "pl-10" : ""}
          ${!!iconRight ? "pr-9" : ""} ${className ?? "w-full"}`}
          onChange={(event) => {
            setCurrentSelected(event.value);
            setValue && setValue(event.value);
            onChange && onChange(event);
            setErrors(undefined);
          }}
          aria-describedby={`${name}-error`}
          options={options}
          optionLabel="label"
          optionValue="value"
          itemTemplate={(option: IDropdown) => (
            <p id={`option-${option.value}`} className="text-sm">
              {option.label}
            </p>
          )}
          pt={{
            root: { className: montserratFont.className },
            input: {
              className: montserratFont.className + " p-0 flex items-center text-sm",
            },
            item: { className: montserratFont.className + " text-sm" },
          }}
          placeholder={placeholder}
          {...props}
        />
        <label htmlFor={name} className={`absolute h-fit truncate ${!!iconLeft ? "left-10" : "left-4"}`}>
          <span className="flex gap-1">
            {label}
            {showRequiredIcon && <span className="text-[--error]"> *</span>}
          </span>
        </label>
        {iconRight ? <i className="absolute right-2 top-1/2 w-fit -mt-[0.5rem]">{iconRight}</i> : null}
      </span>
      {errors && (
        <div id={`${name}-error`} aria-live="polite" aria-atomic="true">
          {errors.map((error: string) => (
            <p className="mt-2 text-xs text-[--error]" key={error}>
              {error}
            </p>
          ))}
        </div>
      )}
    </div>
  );
};

export default Dropdown;
