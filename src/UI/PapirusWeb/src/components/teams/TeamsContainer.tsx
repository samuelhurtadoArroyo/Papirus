"use client";

import useInputSearch from "@/hooks/useInputSearch";
import TeamsHeader from "./TeamsHeader";
import TeamsTable from "./TeamsTable";
import React, { useState } from "react";
import useMyBoolean from "@/hooks/useMyBoolean";
import { ITeam } from "@/domain/interfaces/ITeam";
import { ITeamMember } from "@/domain/interfaces/ITeamMember";
import { TeamMemberDto } from "@/domain/entities/data-contracts";

const TeamsContainer = () => {
  const { debouncedValue, setInputValue, clearValue, inputValue } = useInputSearch();

  const showEditDialog = useMyBoolean(false);
  const [team, setTeam] = useState<ITeam | undefined>();
  const [teamMembers, setTeamMembers] = useState<ITeamMember[] | TeamMemberDto[] | undefined[] | undefined>();

  return (
    <React.Fragment>
      <TeamsHeader
        inputValue={inputValue}
        setInputValue={setInputValue}
        clearValue={clearValue}
        showEditDialog={showEditDialog}
        setTeam={setTeam}
        setTeamMembers={setTeamMembers}
      />
      <TeamsTable debouncedValue={debouncedValue} showEditDialog={showEditDialog} team={team} setTeam={setTeam} teamMembers={teamMembers} setTeamMembers={setTeamMembers} />
    </React.Fragment>
  );
};

export default TeamsContainer;
