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

import { ErrorDetails, FilterParams, FirmDto, SortingParams } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Firms<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Firms
   * @name V10FirmsList
   * @request GET:/api/v1.0/Firms
   * @secure
   */
  v10FirmsList = (
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
    this.request<FirmDto[], ErrorDetails>({
      path: `/api/v1.0/Firms`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Firms
   * @name V10FirmsCreate
   * @request POST:/api/v1.0/Firms
   * @secure
   */
  v10FirmsCreate = (data: FirmDto, params: RequestParams = {}) =>
    this.request<FirmDto, ErrorDetails>({
      path: `/api/v1.0/Firms`,
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
   * @tags Firms
   * @name V10FirmsUpdate
   * @request PUT:/api/v1.0/Firms
   * @secure
   */
  v10FirmsUpdate = (data: FirmDto, params: RequestParams = {}) =>
    this.request<FirmDto, ErrorDetails>({
      path: `/api/v1.0/Firms`,
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
   * @tags Firms
   * @name V10FirmsDetail
   * @request GET:/api/v1.0/Firms/{id}
   * @secure
   */
  v10FirmsDetail = (id: number, params: RequestParams = {}) =>
    this.request<FirmDto, ErrorDetails>({
      path: `/api/v1.0/Firms/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Firms
   * @name V10FirmsDelete
   * @request DELETE:/api/v1.0/Firms/{id}
   * @secure
   */
  v10FirmsDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Firms/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
}
