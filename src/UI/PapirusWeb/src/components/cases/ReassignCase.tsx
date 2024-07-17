"use client";
import { ColumnEditorOptions } from "primereact/column";
import { DropdownChangeEvent } from "primereact/dropdown";
import React from "react";
import TableRowDropdown from "../common/TableRowDropdown";
import { ITeamMemberAssignment } from "@/domain/interfaces/ITeamMember";

const ReassignCase = ({
  editorOptions,
  dropdownOptions,
  emptyText,
}: {
  editorOptions: ColumnEditorOptions;
  dropdownOptions?: ITeamMemberAssignment[];
  emptyText: string;
}) => {
  const getCapacity = (value: ITeamMemberAssignment) => {
    const assignedCases = value?.caseLoad?.split("/")[0]?.trim() || 0;
    const maxCases = value?.caseLoad?.split("/")[1]?.trim() || 0;

    if (maxCases > assignedCases) {
      return "bg-[--success]";
    } else {
      return "bg-[--error]";
    }
  };

  const capacityItemTemplate = (option: ITeamMemberAssignment) => {
    return (
      <p
        id={`option-${option?.memberId}-p`}
        className={`flex gap-5 justify-between rounded-md p-1 w-full text-[--white] text-xs
        ${getCapacity(option)}`}>
        <span>{option?.fullName}</span>
        <span>{option?.caseLoad}</span>
      </p>
    );
  };

  return (
    <TableRowDropdown
      id={`reassign-${editorOptions.rowData?.id}-dropdown`}
      value={editorOptions.value}
      options={dropdownOptions}
      onChange={(e: DropdownChangeEvent) =>
        editorOptions?.editorCallback!(e.value)
      }
      placeholder={!editorOptions.value ? emptyText : undefined}
      optionLabel="fullName"
      optionValue="memberId"
      itemTemplate={capacityItemTemplate}
    />
  );
};


export default ReassignCase;
