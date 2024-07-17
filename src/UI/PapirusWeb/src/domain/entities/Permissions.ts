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

import { ErrorDetails, FilterParams, PermissionDto, SortingParams } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Permissions<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Permissions
   * @name V10PermissionsList
   * @request GET:/api/v1.0/Permissions
   * @secure
   */
  v10PermissionsList = (
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
    this.request<PermissionDto[], ErrorDetails>({
      path: `/api/v1.0/Permissions`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Permissions
   * @name V10PermissionsCreate
   * @request POST:/api/v1.0/Permissions
   * @secure
   */
  v10PermissionsCreate = (data: PermissionDto, params: RequestParams = {}) =>
    this.request<PermissionDto, ErrorDetails>({
      path: `/api/v1.0/Permissions`,
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
   * @tags Permissions
   * @name V10PermissionsUpdate
   * @request PUT:/api/v1.0/Permissions
   * @secure
   */
  v10PermissionsUpdate = (data: PermissionDto, params: RequestParams = {}) =>
    this.request<PermissionDto, ErrorDetails>({
      path: `/api/v1.0/Permissions`,
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
   * @tags Permissions
   * @name V10PermissionsDetail
   * @request GET:/api/v1.0/Permissions/{id}
   * @secure
   */
  v10PermissionsDetail = (id: number, params: RequestParams = {}) =>
    this.request<PermissionDto, ErrorDetails>({
      path: `/api/v1.0/Permissions/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Permissions
   * @name V10PermissionsDelete
   * @request DELETE:/api/v1.0/Permissions/{id}
   * @secure
   */
  v10PermissionsDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Permissions/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Permissions
   * @name V10PermissionsGetByUserList
   * @request GET:/api/v1.0/Permissions/GetByUser
   * @secure
   */
  v10PermissionsGetByUserList = (params: RequestParams = {}) =>
    this.request<PermissionDto[], ErrorDetails>({
      path: `/api/v1.0/Permissions/GetByUser`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
}
