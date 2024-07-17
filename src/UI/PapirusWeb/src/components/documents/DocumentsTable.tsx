"use client";
import { textConstants } from "@/domain/globalization/es";
import { IDocument } from "@/domain/interfaces/IDocument";
import useLazyTable from "@/hooks/useLazyTable";
import Image from "next/image";
import Link from "next/link";
import { Column } from "primereact/column";
import Table from "../common/Table";
import { usePermissions } from "@/hooks/usePermissions";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";

const DocumentsTable = ({
  documents,
  guardianshipId,
}: {
  documents: IDocument[];
  guardianshipId: number;
}) => {
  const { lazyState, onPage, onSort, onFilter, setRows } = useLazyTable([
    "name",
  ]);
  const { validatePermission, permissionConstants } = usePermissions();
  const { headers } = textConstants.pages.documents.table;
  const altText = textConstants.components.alt.icons;

  return (
    <div className="flex items-center justify-between bg-[--white] w-full rounded-lg shadow-md">
      <Table
        id="guardianships-data-table"
        dataKey="id"
        lazy={false}
        tableStyle={{ minWidth: "10rem" }}
        value={documents}
        loading={false}
        first={lazyState.first}
        onPage={onPage}
        onSort={onSort}
        sortField={lazyState.sortField}
        sortOrder={lazyState.sortOrder}
        onFilter={onFilter}
        filters={lazyState.filters}
        paginator={false}
        onRowsPerPageChange={setRows}>
        <Column field="fileName" header={headers.documents} sortable />
        <Column
          field="actions"
          header={headers.actions}
          headerClassName="min-w-[72px] w-[6%]"
          rowEditor={true}
          bodyClassName="text-center h-full p-1"
          body={(rowData: IDocument) => {
            return (
              <div className="flex justify-center w-full h-full items-center gap-2">
                <Link
                  id={`view-document-${rowData?.id}-a`}
                  href={`/guardianships/${guardianshipId}/documents/${rowData?.id}`}
                  className="papirus-icon-button"
                  {...getDisabledLinkProps(
                    !validatePermission(permissionConstants.document.view)
                  )}>
                  <Image
                    src="/view.svg"
                    alt={altText.view}
                    width={15}
                    height={11}
                  />
                </Link>
              </div>
            );
          }}
        />
      </Table>
    </div>
  );
};

export default DocumentsTable;
