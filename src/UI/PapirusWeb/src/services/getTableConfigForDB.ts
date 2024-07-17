import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import { getSortForDB } from "./getSortForDB";
import { IFilters } from "@/domain/interfaces/IFilters";
import { getFiltersForDB } from "./getFiltersForDB";

export const getTableConfigForDB = (lazyState: ILazyTableState): IFilters => {
  return {
    PageNumber: lazyState.page,
    PageSize: lazyState.rows,
    FilterParams: getFiltersForDB(lazyState),
    SortingParams: lazyState.sortField
      ? [
          {
            columnName: lazyState.sortField,
            sortOrder: getSortForDB(lazyState.sortOrder),
          },
        ]
      : undefined,
  };
};
