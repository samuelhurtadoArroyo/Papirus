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

import { ErrorDetails, FilterParams, SortingParams, TeamDto, TeamMemberDto } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Teams<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Teams
   * @name V10TeamsList
   * @request GET:/api/v1.0/Teams
   * @secure
   */
  v10TeamsList = (
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
    this.request<TeamDto[], ErrorDetails>({
      path: `/api/v1.0/Teams`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name V10TeamsCreate
   * @request POST:/api/v1.0/Teams
   * @secure
   */
  v10TeamsCreate = (data: TeamDto, params: RequestParams = {}) =>
    this.request<TeamDto, ErrorDetails>({
      path: `/api/v1.0/Teams`,
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
   * @tags Teams
   * @name V10TeamsUpdate
   * @request PUT:/api/v1.0/Teams
   * @secure
   */
  v10TeamsUpdate = (data: TeamDto, params: RequestParams = {}) =>
    this.request<TeamDto, ErrorDetails>({
      path: `/api/v1.0/Teams`,
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
   * @tags Teams
   * @name V10TeamsDetail
   * @request GET:/api/v1.0/Teams/{id}
   * @secure
   */
  v10TeamsDetail = (id: number, params: RequestParams = {}) =>
    this.request<TeamDto, ErrorDetails>({
      path: `/api/v1.0/Teams/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name V10TeamsDelete
   * @request DELETE:/api/v1.0/Teams/{id}
   * @secure
   */
  v10TeamsDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Teams/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name V10TeamsMembersDetail
   * @request GET:/api/v1.0/Teams/{id}/members
   * @secure
   */
  v10TeamsMembersDetail = (id: number, params: RequestParams = {}) =>
    this.request<TeamMemberDto[], ErrorDetails>({
      path: `/api/v1.0/Teams/${id}/members`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
}
