import { FilterParams, SortingParams } from "../entities/data-contracts";

export interface IFilters {
  PageNumber?: number;
  PageSize?: number;
  SearchString?: string;
  FilterParams?: FilterParams[];
  SortingParams?: SortingParams[];
}
