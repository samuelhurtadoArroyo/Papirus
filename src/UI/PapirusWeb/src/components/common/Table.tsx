import { getPaginatorTemplate } from "@/services/getPaginatorTemplate";
import {
  DataTable,
  DataTableBaseProps,
  DataTableFilterMeta,
  DataTableValueArray,
} from "primereact/datatable";
import { Montserrat } from "next/font/google";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
const montserratFont = Montserrat({ subsets: ["latin"] });

const Table = ({
  children,
  onRowsPerPageChange,
  rows,
  filters,
  paginator = true,
  ...props
}: {
  onRowsPerPageChange?: (row: number) => void;
  rows?: number;
  filters?: ILazyTableState["filters"];
} & Omit<DataTableBaseProps<DataTableValueArray>, "filters">) => {
  return (
    <DataTable
      lazy
      editMode="row"
      rows={rows}
      dataKey="id"
      emptyMessage
      paginator={paginator}
      tableClassName="w-full"
      className="rounded-t-lg w-full text-xs"
      tableStyle={{ minWidth: "50rem" }}
      paginatorTemplate={
        onRowsPerPageChange && paginator
          ? getPaginatorTemplate({
              onRowsPerPageChange,
              rows,
            })
          : undefined
      }
      filters={filters as ILazyTableState["filters"] & DataTableFilterMeta}
      paginatorClassName={`rounded-b-lg flex w-full justify-end text-xs`}
      pt={{
        root: {
          className: montserratFont.className,
        },
        wrapper: {
          className: "rounded-t-lg",
        },
        paginator: {
          root: {
            className: montserratFont.className,
          },
        },
        column: {
          headerCell: { className: "font-semibold bg-[--table-header]" },
        },
      }}
      {...props}>
      {children}
    </DataTable>
  );
};

export default Table;
