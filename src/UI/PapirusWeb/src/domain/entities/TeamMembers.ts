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

import { ErrorDetails, FilterParams, SortingParams, TeamMemberDto } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class TeamMembers<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags TeamMembers
   * @name V10TeamMembersList
   * @request GET:/api/v1.0/TeamMembers
   * @secure
   */
  v10TeamMembersList = (
    query?: {
      /** @format int32 */
      PageNumber?: number;
      /** @format int32 */
      PageSize?: number;
      SearchString?: string;
      FilterParams?: FilterParams[];
      SortingParams?: SortingParams[];
    },
    params: RequestParams = {},
  ) =>
    this.request<TeamMemberDto[], ErrorDetails>({
      path: `/api/v1.0/TeamMembers`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags TeamMembers
   * @name V10TeamMembersCreate
   * @request POST:/api/v1.0/TeamMembers
   * @secure
   */
  v10TeamMembersCreate = (data: TeamMemberDto, params: RequestParams = {}) =>
    this.request<TeamMemberDto, ErrorDetails>({
      path: `/api/v1.0/TeamMembers`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags TeamMembers
   * @name V10TeamMembersUpdate
   * @request PUT:/api/v1.0/TeamMembers
   * @secure
   */
  v10TeamMembersUpdate = (data: TeamMemberDto, params: RequestParams = {}) =>
    this.request<TeamMemberDto, ErrorDetails>({
      path: `/api/v1.0/TeamMembers`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags TeamMembers
   * @name V10TeamMembersDetail
   * @request GET:/api/v1.0/TeamMembers/{id}
   * @secure
   */
  v10TeamMembersDetail = (id: number, params: RequestParams = {}) =>
    this.request<TeamMemberDto, ErrorDetails>({
      path: `/api/v1.0/TeamMembers/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags TeamMembers
   * @name V10TeamMembersDelete
   * @request DELETE:/api/v1.0/TeamMembers/{id}
   * @secure
   */
  v10TeamMembersDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/TeamMembers/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
}
