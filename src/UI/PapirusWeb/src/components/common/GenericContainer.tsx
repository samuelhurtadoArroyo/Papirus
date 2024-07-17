import React from "react";
import GenericHeader from "./GenericHeader";
import GenericDataTable from "./GenericDataTable";
import useMyString from "@/hooks/useMyString";
import useMyBoolean from "@/hooks/useMyBoolean";

export default function GenericContainer({ headerLabel, emptyValue, setValue }: { headerLabel: string; emptyValue: any; setValue: any }) {
  const searchValue = useMyString();
  const showFormDialog = useMyBoolean(false);

  return (
    <React.Fragment>
      <GenericHeader
        headerLabel={headerLabel}
        search={searchValue.data}
        setSearch={searchValue.setData}
        clearSearch={searchValue.resetData}
        showFormDialog={showFormDialog}
        emptyValue={emptyValue}
        setValue={setValue}
      />

      <GenericDataTable></GenericDataTable>
    </React.Fragment>
  );
}
