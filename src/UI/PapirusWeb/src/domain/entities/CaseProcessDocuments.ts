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

import { CaseProcessDocumentDto, ErrorDetails } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class CaseProcessDocuments<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CaseProcessDocuments
   * @name V10CaseProcessDocumentsList
   * @request GET:/api/v1.0/CaseProcessDocuments
   * @secure
   */
  v10CaseProcessDocumentsList = (
    query?: {
      /** @format int32 */
      caseId?: number;
    },
    params: RequestParams = {},
  ) =>
    this.request<CaseProcessDocumentDto[], ErrorDetails>({
      path: `/api/v1.0/CaseProcessDocuments`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags CaseProcessDocuments
   * @name V10CaseProcessDocumentsDetail
   * @request GET:/api/v1.0/CaseProcessDocuments/{id}
   * @secure
   */
  v10CaseProcessDocumentsDetail = (id: number, params: RequestParams = {}) =>
    this.request<CaseProcessDocumentDto, ErrorDetails>({
      path: `/api/v1.0/CaseProcessDocuments/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
}
