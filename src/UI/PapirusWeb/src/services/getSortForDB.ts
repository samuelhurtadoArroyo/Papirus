import { SortingParams } from "@/domain/entities/data-contracts";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";

export const getSortForDB = (
  sortOrder: ILazyTableState["sortOrder"]
): SortingParams["sortOrder"] => {
  if (sortOrder === 1) {
    return "Asc";
  } else if (sortOrder === -1) {
    return "Desc";
  }
  return undefined;
};
