"use client";
import { textConstants } from "@/domain/globalization/es";
import useInputSearch from "@/hooks/useInputSearch";
import Image from "next/image";
import { useSearchParams } from "next/navigation";
import { useCallback, useEffect } from "react";
import InputText from "./InputText";

const searchText = textConstants.components.common.search;

const InputSearchParams = ({
  id,
  name = "search",
  placeholder,
  label = searchText,
  disabled
}: {
  id?: string;
  name?: string;
  placeholder?: string;
  label?: string;
  disabled?: boolean;
}) => {
  const searchParams = useSearchParams();
  const defaultValue = searchParams.get("query") || "";
  const { debouncedValue, setInputValue, clearValue, inputValue } =
    useInputSearch(defaultValue);

  const altIconText = textConstants.components.alt.icons;
  const updateState = useCallback(() => {
    const params = new URLSearchParams(searchParams);
    if (debouncedValue) {
      params.set("query", debouncedValue);
    } else {
      params.delete("query");
    }
    window.history.replaceState(null, "", `?${params.toString()}`);
  }, [debouncedValue, searchParams]);

  useEffect(() => {
    updateState();
  }, [updateState]);

  const handleClearSearch = () => {
    clearValue();
  };

  return (
    <InputText
      id={id}
      setValue={setInputValue}
      value={inputValue}
      disabled={disabled}
      iconLeft={
        <Image
          src={"/search.svg"}
          alt={altIconText.search}
          height={18}
          width={18}
        />
      }
      iconRight={
        !!inputValue && (
          <button onClick={handleClearSearch}>
            <Image
              src={"/clear.svg"}
              alt={altIconText.clear}
              height={18}
              width={18}
            />
          </button>
        )
      }
      name={name}
      type={"text"}
      label={label}
      placeholder={placeholder}
      className="w-44 md:w-[210px]"
    />
  );
};

export default InputSearchParams;
