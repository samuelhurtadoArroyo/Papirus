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

import { ErrorDetails, FilterParams, PersonDto, SortingParams } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class People<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags People
   * @name V10PeopleList
   * @request GET:/api/v1.0/People
   * @secure
   */
  v10PeopleList = (
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
    this.request<PersonDto[], ErrorDetails>({
      path: `/api/v1.0/People`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags People
   * @name V10PeopleCreate
   * @request POST:/api/v1.0/People
   * @secure
   */
  v10PeopleCreate = (data: PersonDto, params: RequestParams = {}) =>
    this.request<PersonDto, ErrorDetails>({
      path: `/api/v1.0/People`,
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
   * @tags People
   * @name V10PeopleUpdate
   * @request PUT:/api/v1.0/People
   * @secure
   */
  v10PeopleUpdate = (data: PersonDto, params: RequestParams = {}) =>
    this.request<PersonDto, ErrorDetails>({
      path: `/api/v1.0/People`,
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
   * @tags People
   * @name V10PeopleDetail
   * @request GET:/api/v1.0/People/{id}
   * @secure
   */
  v10PeopleDetail = (id: number, params: RequestParams = {}) =>
    this.request<PersonDto, ErrorDetails>({
      path: `/api/v1.0/People/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags People
   * @name V10PeopleDelete
   * @request DELETE:/api/v1.0/People/{id}
   * @secure
   */
  v10PeopleDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/People/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
}
