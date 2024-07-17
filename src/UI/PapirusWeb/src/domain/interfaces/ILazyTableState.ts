import { FilterParams } from "../entities/data-contracts";

export interface ILazyTableState {
  first?: number;
  rows?: number;
  page?: number;
  sortField?: string;
  sortOrder?: 1 | 0 | -1 | null | undefined;
  filters?: {
    [key: string]: {
      value: any;
      matchMode?: FilterParams["filterOption"];
    };
  };
}
