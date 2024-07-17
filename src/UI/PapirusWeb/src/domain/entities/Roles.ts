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

import { ErrorDetails, FilterParams, RoleDto, SortingParams } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Roles<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Roles
   * @name V10RolesList
   * @request GET:/api/v1.0/Roles
   * @secure
   */
  v10RolesList = (
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
    this.request<RoleDto[], ErrorDetails>({
      path: `/api/v1.0/Roles`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Roles
   * @name V10RolesCreate
   * @request POST:/api/v1.0/Roles
   * @secure
   */
  v10RolesCreate = (data: RoleDto, params: RequestParams = {}) =>
    this.request<RoleDto, ErrorDetails>({
      path: `/api/v1.0/Roles`,
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
   * @tags Roles
   * @name V10RolesUpdate
   * @request PUT:/api/v1.0/Roles
   * @secure
   */
  v10RolesUpdate = (data: RoleDto, params: RequestParams = {}) =>
    this.request<RoleDto, ErrorDetails>({
      path: `/api/v1.0/Roles`,
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
   * @tags Roles
   * @name V10RolesDetail
   * @request GET:/api/v1.0/Roles/{id}
   * @secure
   */
  v10RolesDetail = (id: number, params: RequestParams = {}) =>
    this.request<RoleDto, ErrorDetails>({
      path: `/api/v1.0/Roles/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Roles
   * @name V10RolesDelete
   * @request DELETE:/api/v1.0/Roles/{id}
   * @secure
   */
  v10RolesDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Roles/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
}
