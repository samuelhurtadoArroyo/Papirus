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

import { ErrorDetails, LoginDto, LoginInputDto } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Authentication<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Authentication
   * @name V10AuthenticationLoginCreate
   * @request POST:/api/v1.0/Authentication/login
   * @secure
   */
  v10AuthenticationLoginCreate = (data: LoginInputDto, params: RequestParams = {}) =>
    this.request<LoginDto, ErrorDetails>({
      path: `/api/v1.0/Authentication/login`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
}
