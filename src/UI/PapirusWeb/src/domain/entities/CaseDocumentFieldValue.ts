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

import {
  ActionResult,
  CaseDocumentFieldValueDto,
  ErrorDetails,
  UpdateCaseDocumentFieldValueDto,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class CaseDocumentFieldValue<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CaseDocumentFieldValue
   * @name V10CaseDocumentFieldValueDetail
   * @request GET:/api/v1.0/CaseDocumentFieldValue/{caseId}
   * @secure
   */
  v10CaseDocumentFieldValueDetail = (
    caseId: number,
    query?: {
      /** @format int32 */
      documentTypeId?: number;
    },
    params: RequestParams = {},
  ) =>
    this.request<CaseDocumentFieldValueDto[], ErrorDetails | ActionResult>({
      path: `/api/v1.0/CaseDocumentFieldValue/${caseId}`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags CaseDocumentFieldValue
   * @name V10CaseDocumentFieldValueUpdate
   * @request PUT:/api/v1.0/CaseDocumentFieldValue
   * @secure
   */
  v10CaseDocumentFieldValueUpdate = (data: UpdateCaseDocumentFieldValueDto[], params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/CaseDocumentFieldValue`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
}
