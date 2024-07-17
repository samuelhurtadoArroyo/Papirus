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

import { ErrorDetails, FilterParams, SortingParams, UserDto, UserInputDto } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Users<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Users
   * @name V10UsersList
   * @request GET:/api/v1.0/Users
   * @secure
   */
  v10UsersList = (
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
    this.request<UserDto[], ErrorDetails>({
      path: `/api/v1.0/Users`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Users
   * @name V10UsersCreate
   * @request POST:/api/v1.0/Users
   * @secure
   */
  v10UsersCreate = (data: UserInputDto, params: RequestParams = {}) =>
    this.request<UserDto, ErrorDetails>({
      path: `/api/v1.0/Users`,
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
   * @tags Users
   * @name V10UsersUpdate
   * @request PUT:/api/v1.0/Users
   * @secure
   */
  v10UsersUpdate = (data: UserDto, params: RequestParams = {}) =>
    this.request<UserDto, ErrorDetails>({
      path: `/api/v1.0/Users`,
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
   * @tags Users
   * @name V10UsersDetail
   * @request GET:/api/v1.0/Users/{id}
   * @secure
   */
  v10UsersDetail = (id: number, params: RequestParams = {}) =>
    this.request<UserDto, ErrorDetails>({
      path: `/api/v1.0/Users/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Users
   * @name V10UsersDelete
   * @request DELETE:/api/v1.0/Users/{id}
   * @secure
   */
  v10UsersDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Users/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
}
