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

import { HttpClient, RequestParams } from "./http-client";

export class HealthCheck<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags HealthCheck
   * @name V10HealthCheckList
   * @request GET:/api/v1.0/HealthCheck
   * @secure
   */
  v10HealthCheckList = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/v1.0/HealthCheck`,
      method: "GET",
      secure: true,
      ...params,
    });
}
