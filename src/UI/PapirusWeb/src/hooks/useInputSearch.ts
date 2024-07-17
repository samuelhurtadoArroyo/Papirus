import { INPUT_CONSTANTS } from "@/domain/constants/components";
import { useDebounce } from "primereact/hooks";

const useInputSearch = (defaultValue?: string) => {
  const [inputValue, debouncedValue, setInputValue] = useDebounce(
    defaultValue || "",
    INPUT_CONSTANTS.debounceDelay
  );

  const clearValue = () => {
    setInputValue("");
  };

  return {
    inputValue,
    setInputValue,
    clearValue,
    debouncedValue,
  };
};

export default useInputSearch;
