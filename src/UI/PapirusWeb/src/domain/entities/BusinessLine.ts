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

import { BusinessLineDto, ErrorDetails } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class BusinessLine<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags BusinessLine
   * @name V10BusinessLineList
   * @request GET:/api/v1.0/BusinessLine
   * @secure
   */
  v10BusinessLineList = (params: RequestParams = {}) =>
    this.request<BusinessLineDto[], ErrorDetails>({
      path: `/api/v1.0/BusinessLine`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
}
