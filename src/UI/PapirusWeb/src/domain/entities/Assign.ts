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

import { CaseAssignmentDto } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class Assign<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CaseAssignment
   * @name CaseAssignmentAssignCreate
   * @request POST:/api/CaseAssignment/assign
   * @secure
   */
  caseAssignmentAssignCreate = (
    query?: {
      /** @format int32 */
      caseId?: number;
      /** @format int32 */
      userId?: number;
      /** @format int32 */
      caseStatus?: number;
    },
    params: RequestParams = {}
  ) =>
    this.request<CaseAssignmentDto, any>({
      path: `/api/v1.0/CaseAssignment/assign`,
      method: "POST",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
}
