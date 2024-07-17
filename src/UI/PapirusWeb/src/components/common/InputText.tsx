"use client";
import React, {
  ChangeEventHandler,
  Dispatch,
  ReactNode,
  useEffect,
  useState,
} from "react";
import { InputText as PrimeInputText } from "primereact/inputtext";

const InputText = ({
  id,
  value,
  label,
  defaultValue,
  setValue,
  onChange,
  name,
  type,
  required,
  placeholder,
  iconRight,
  iconLeft,
  className,
  showRequiredIcon,
  errorMessages,
  containerClassName,
  disabled,
}: {
  id?: string;
  value?: string;
  label: string;
  defaultValue?: string | null;
  setValue?: Dispatch<string>;
  onChange?: ChangeEventHandler<HTMLInputElement>;
  name: string;
  type: React.HTMLInputTypeAttribute;
  required?: boolean;
  placeholder?: string;
  iconRight?: ReactNode;
  iconLeft?: ReactNode;
  className?: string;
  showRequiredIcon?: boolean;
  errorMessages?: string[];
  containerClassName?: string;
  disabled?: boolean;
}) => {
  const [currentValue, setCurrentValue] = useState(defaultValue || "");
  const [errors, setErrors] = useState(errorMessages);

  useEffect(() => {
    errorMessages && setErrors(errorMessages);

    return () => {
      setErrors(undefined);
    };
  }, [errorMessages]);

  useEffect(() => {
    value && setCurrentValue(value);
  }, [value]);

  return (
    <div className={`flex flex-col ${containerClassName ?? "w-auto"}`}>
      <span
        className={`p-float-label relative w-full ${
          !!iconRight ? "p-input-icon-right" : ""
        } ${!!iconLeft ? "p-input-icon-left" : ""}`}>
        {iconLeft ? (
          <i className="absolute left-4 top-1/2 w-fit -mt-[0.5rem]">
            {iconLeft}
          </i>
        ) : null}
        <PrimeInputText
          id={id ?? name}
          name={name}
          type={type}
          placeholder={placeholder}
          value={value ?? currentValue}
          onChange={(event) => {
            setCurrentValue(event.currentTarget.value);
            setValue && setValue(event.currentTarget.value);
            onChange && onChange(event);
            setErrors(undefined);
          }}
          required={required}
          aria-describedby={`${name}-error`}
          aria-disabled={disabled}
          disabled={disabled}
          pt={{
            root: {
              className: `text-sm h-10 border-2 placeholder-transparent focus:placeholder-[--papirus-grey] ${
                errors ? "border-[--error] shadow-none" : ""
              } rounded-[4px] px-5 py-0 [&:focus+label]:left-4 ${
                !!iconLeft ? "pl-10" : ""
              } ${!!iconRight ? "pr-9" : ""} ${className ?? "w-full"}`,
            },
          }}
        />
        <label
          htmlFor={name}
          className={`absolute h-fit truncate ${
            !!iconLeft ? "left-10" : "left-4"
          } ${disabled ? "text-[--papirus-grey]" : ""} ${
            errors ? "text-[--error]" : ""
          }`}>
          <span className="flex gap-1">
            {label}
            {showRequiredIcon && <span className="text-[--error]"> *</span>}
          </span>
        </label>
        {iconRight ? (
          <i className="absolute right-2 top-1/2 w-fit -mt-[0.5rem]">
            {iconRight}
          </i>
        ) : null}
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

export default InputText;
