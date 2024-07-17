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

import { ActorDto, ErrorDetails, FilterParams, SortingParams } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Actors<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Actors
   * @name V10ActorsList
   * @request GET:/api/v1.0/Actors
   * @secure
   */
  v10ActorsList = (
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
    this.request<ActorDto[], ErrorDetails>({
      path: `/api/v1.0/Actors`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Actors
   * @name V10ActorsCreate
   * @request POST:/api/v1.0/Actors
   * @secure
   */
  v10ActorsCreate = (data: ActorDto, params: RequestParams = {}) =>
    this.request<ActorDto, ErrorDetails>({
      path: `/api/v1.0/Actors`,
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
   * @tags Actors
   * @name V10ActorsUpdate
   * @request PUT:/api/v1.0/Actors
   * @secure
   */
  v10ActorsUpdate = (data: ActorDto, params: RequestParams = {}) =>
    this.request<ActorDto, ErrorDetails>({
      path: `/api/v1.0/Actors`,
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
   * @tags Actors
   * @name V10ActorsDetail
   * @request GET:/api/v1.0/Actors/{id}
   * @secure
   */
  v10ActorsDetail = (id: number, params: RequestParams = {}) =>
    this.request<ActorDto, ErrorDetails>({
      path: `/api/v1.0/Actors/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Actors
   * @name V10ActorsDelete
   * @request DELETE:/api/v1.0/Actors/{id}
   * @secure
   */
  v10ActorsDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Actors/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
}
