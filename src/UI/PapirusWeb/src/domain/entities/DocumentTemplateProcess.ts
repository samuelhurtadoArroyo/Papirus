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

export class DocumentTemplateProcess<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags DocumentTemplateProcess
   * @name V10DocumentTemplateProcessCreate
   * @request POST:/api/v1.0/DocumentTemplateProcess/{caseId}
   * @secure
   */
  v10DocumentTemplateProcessCreate = (
    caseId: number,
    query?: {
      /** @format int32 */
      templateId?: number;
      /** @format int32 */
      documenType?: number;
    },
    params: RequestParams = {},
  ) =>
    this.request<CaseProcessDocumentDto, ErrorDetails>({
      path: `/api/v1.0/DocumentTemplateProcess/${caseId}`,
      method: "POST",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
}
