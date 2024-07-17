import {
  PAGINATION_CONSTANTS,
  TABLE_FILTER_CONSTANTS,
  TABLE_SORT_CONSTANTS,
} from "@/domain/constants/components";
import { textConstants } from "@/domain/globalization/es";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import {
  DataTableFilterEvent,
  DataTablePageEvent,
  DataTableSortEvent,
} from "primereact/datatable";
import { useState } from "react";

const useLazyTable = (fieldNames: string[]) => {
  const defaultFilters: ILazyTableState["filters"] = {};
  fieldNames.forEach((fieldName) => {
    defaultFilters[fieldName] = {
      value: null,
      matchMode: "IsEqualTo"
    };
  });
  const [isLoading, setIsLoading] = useState(true);
  const [totalRecords, setTotalRecords] = useState(0);
  const [lazyState, setLazyState] = useState<ILazyTableState>({
    first: PAGINATION_CONSTANTS.firstIndex,
    rows: PAGINATION_CONSTANTS.defaultSelectedRowsPerPage,
    page: PAGINATION_CONSTANTS.defaultFirstPage,
    sortField: TABLE_SORT_CONSTANTS.defaultSortField,
    sortOrder:
      TABLE_SORT_CONSTANTS.defaultSortOrder as ILazyTableState["sortOrder"],
    filters: defaultFilters,
  });

  const getMatchModeOptions = (
    type: keyof typeof TABLE_FILTER_CONSTANTS.matchModeOptions
  ) =>
    TABLE_FILTER_CONSTANTS.matchModeOptions[type].map((option) => ({
      value: option,
      label:
        textConstants.filters.matchMode[
          option as keyof typeof textConstants.filters.matchMode
        ],
    }));

  const onPage = (event: DataTablePageEvent) => {
    setLazyState((prevState) => ({
      ...prevState,
      ...event,
      page: event.first / event.rows + 1,
    }));
  };

  const onSort = (event: DataTableSortEvent) => {
    setLazyState((prevState) => ({ ...prevState, ...event }));
  };

  const setRows = (rows: number) => {
    setLazyState((prevState) => ({ ...prevState, rows, page: 1, first: 0 }));
  };

  const onFilter = (event: DataTableFilterEvent) => {
    setLazyState((prevState) => ({
      ...prevState,
      ...event,
      filters: event.filters as ILazyTableState["filters"],
      page: 1,
      first: 0,
    }));
  };

  return {
    lazyState,
    totalRecords,
    setTotalRecords,
    isLoading,
    setIsLoading,
    onPage,
    onSort,
    onFilter,
    setRows,
    setLazyState,
    matchModeTextOptions: getMatchModeOptions("text"),
    matchModeCompareOptions: getMatchModeOptions("compare"),
    matchModeEqualsOptions: getMatchModeOptions("equals"),
    matchModeListOptions: getMatchModeOptions("list"),
  };
};
export default useLazyTable;
