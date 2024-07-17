"use client";
import { textConstants } from "@/domain/globalization/es";
import React, { Dispatch, SetStateAction } from "react";
import { Button, InputText } from "../common";
import Image from "next/image";
import { ITeam } from "@/domain/interfaces/ITeam";
import { ITeamMember } from "@/domain/interfaces/ITeamMember";
import { usePermissions } from "@/hooks/usePermissions";

const TeamsHeader = ({
  setInputValue,
  clearValue,
  inputValue,
  showEditDialog,
  setTeam,
  setTeamMembers,
}: {
  setInputValue: Dispatch<SetStateAction<string>>;
  clearValue: () => void;
  inputValue: string;
  showEditDialog?: any;
  setTeam: React.Dispatch<React.SetStateAction<ITeam | undefined>>;
  setTeamMembers: React.Dispatch<React.SetStateAction<ITeamMember[] | undefined[] | undefined>>;
}) => {
  const teamHeaderText = textConstants.pages.teams.header;
  const altIconText = textConstants.components.alt.icons;
  const { validatePermission, permissionConstants } = usePermissions();
  const addCallback = () => {
    setTeam({} as ITeam);
    setTeamMembers([] as ITeamMember[]);
    showEditDialog.setTrue();
  };

  return (
    <div className="flex flex-wrap items-center justify-between bg-[--white] px-[30px] h-[90px] w-full rounded-lg shadow-md">
      <h1 className="text-xl font-semibold uppercase">
        {teamHeaderText.title}
      </h1>
      <div className="flex gap-5">
        <InputText
          setValue={setInputValue}
          value={inputValue}
          disabled={!validatePermission(permissionConstants.teams.search)}
          iconLeft={
            <Image
              src={"/search.svg"}
              alt={altIconText.search}
              height={18}
              width={18}
            />
          }
          iconRight={
            !!inputValue && (
              <button onClick={clearValue}>
                <Image
                  src={"/clear.svg"}
                  alt={altIconText.clear}
                  height={18}
                  width={18}
                />
              </button>
            )
          }
          name={"search"}
          type={"text"}
          label={teamHeaderText.search}
          placeholder={teamHeaderText.search}
          className="w-44 md:w-[210px]"
        />
        <Button
          id="add-team-btn"
          label={teamHeaderText.addTeam}
          className="uppercase"
          variant="primary"
          onClick={addCallback}
          disabled={!validatePermission(permissionConstants.teams.create)}
        />
      </div>
    </div>
  );
};

export default TeamsHeader;
