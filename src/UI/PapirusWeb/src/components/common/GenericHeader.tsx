"use client";
import { textConstants } from "@/domain/globalization/es";
import React, { Dispatch, SetStateAction } from "react";
import { Button, InputText } from "../common";
import Image from "next/image";
import { ITeam } from "@/domain/interfaces/ITeam";

const GenericHeader = ({
  headerLabel,
  emptyValue,
  search,
  setSearch,
  clearSearch,
  showFormDialog,
  setValue,
}: {
  headerLabel: string;
  emptyValue: any;
  search: string;
  setSearch: Dispatch<SetStateAction<string>>;
  clearSearch: () => void;
  showFormDialog?: any;
  setValue: any;
}) => {
  const teamHeaderText = textConstants.pages.teams.header;
  const altIconText = textConstants.components.alt.icons;

  const addCallback = () => {
    setValue(emptyValue);
    showFormDialog.setTrue();
  };

  return (
    <div className="flex items-center justify-between bg-[--white] px-[30px] h-[90px] w-full rounded-lg shadow-md">
      <h1 className="text-xl font-semibold uppercase">{headerLabel}</h1>
      <div className="flex gap-5">
        <InputText
          setValue={setSearch}
          value={search}
          iconLeft={<Image src={"/search.svg"} alt={altIconText.search} height={18} width={18} />}
          iconRight={!!search && (
            <button onClick={clearSearch}>
              <Image src={"/clear.svg"} alt={altIconText.clear} height={18} width={18} />
            </button>
          )}
          name={"search"}
          type={"text"}
          placeholder={teamHeaderText.search}
          className="w-44 md:w-[210px]"
          label={""} />
        <Button id="gen-head" label={teamHeaderText.addTeam} className="uppercase" variant="primary" onClick={addCallback} />
      </div>
    </div>
  );
};

export default GenericHeader;
