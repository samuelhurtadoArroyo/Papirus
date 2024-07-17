import { TABLE_FILTER_CONSTANTS } from "@/domain/constants/components";
import { FilterParams } from "@/domain/entities/data-contracts";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";

export const getFiltersForDB = (
  lazyState: ILazyTableState
): FilterParams[] | undefined => {
  if (lazyState.filters === undefined) {
    return undefined;
  }

  const filters =
    Object.entries(lazyState.filters).length > 0
      ? Object.entries(lazyState.filters).map((keyValue) => {
          const [key, valueObject] = keyValue;
          const { value, matchMode } = valueObject;

          const getFilterOption = (
            value: any,
            matchMode: FilterParams["filterOption"]
          ) => {
            if (value === TABLE_FILTER_CONSTANTS.dropdownEmptyIndentifier) {
              if (matchMode === "IsEqualTo") return "IsNull";
              if (matchMode === "IsNotEqualTo") return "IsNotNull";
              return undefined;
            } 
            
            return matchMode || undefined;
          };

          if (value === null || value === undefined) return null;
          return {
            columnName: key,
            filterValue: value,
            filterOption: getFilterOption(value, matchMode),
          };
        })
      : undefined;

  return (
    (filters?.filter((filter) => filter !== null) as FilterParams[]) ||
    undefined
  );
};
