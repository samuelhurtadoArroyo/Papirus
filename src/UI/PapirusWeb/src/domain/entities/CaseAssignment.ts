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

import { CaseAssignmentDto, ErrorDetails, TeamMemberAssignmentDto } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class CaseAssignment<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CaseAssignment
   * @name V10CaseAssignmentAssignCreate
   * @request POST:/api/v1.0/CaseAssignment/assign
   * @secure
   */
  v10CaseAssignmentAssignCreate = (
    query?: {
      /** @format int32 */
      caseId?: number;
      /** @format int32 */
      userId?: number;
      /** @format int32 */
      caseStatus?: number;
    },
    params: RequestParams = {},
  ) =>
    this.request<CaseAssignmentDto, ErrorDetails>({
      path: `/api/v1.0/CaseAssignment/assign`,
      method: "POST",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags CaseAssignment
   * @name V10CaseAssignmentGetListTeamMembersList
   * @request GET:/api/v1.0/CaseAssignment/getListTeamMembers
   * @secure
   */
  v10CaseAssignmentGetListTeamMembersList = (
    query?: {
      /** @format int32 */
      teamMemberId?: number;
    },
    params: RequestParams = {},
  ) =>
    this.request<TeamMemberAssignmentDto[], ErrorDetails>({
      path: `/api/v1.0/CaseAssignment/getListTeamMembers`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
}
