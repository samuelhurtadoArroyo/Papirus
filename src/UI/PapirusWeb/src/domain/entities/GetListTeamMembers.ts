/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import { TeamMemberAssignmentDto } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class GetListTeamMembers<
  SecurityDataType = unknown
> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CaseAssignment
   * @name CaseAssignmentGetListTeamMembersList
   * @request GET:/api/CaseAssignment/getListTeamMembers
   * @secure
   */
  caseAssignmentGetListTeamMembersList = (
    query?: {
      /** @format int32 */
      teamMemberId?: number;
    },
    params: RequestParams = {}
  ) =>
    this.request<TeamMemberAssignmentDto, any>({
      path: `/api/v1.0/CaseAssignment/getListTeamMembers`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
}
