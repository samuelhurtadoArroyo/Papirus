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
  CaseDto,
  CaseWithAssignmentDto,
  EmailPreviewDto,
  ErrorDetails,
  FilterParams,
  GuardianshipDto,
  SortingParams,
  UpdateBusinessLineInputDto,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Cases<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesList
   * @request GET:/api/v1.0/Cases
   * @secure
   */
  v10CasesList = (
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
    this.request<CaseDto[], ErrorDetails>({
      path: `/api/v1.0/Cases`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesCreate
   * @request POST:/api/v1.0/Cases
   * @secure
   */
  v10CasesCreate = (data: CaseDto, params: RequestParams = {}) =>
    this.request<CaseDto, ErrorDetails>({
      path: `/api/v1.0/Cases`,
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
   * @tags Cases
   * @name V10CasesUpdate
   * @request PUT:/api/v1.0/Cases
   * @secure
   */
  v10CasesUpdate = (data: CaseDto, params: RequestParams = {}) =>
    this.request<CaseDto, ErrorDetails>({
      path: `/api/v1.0/Cases`,
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
   * @tags Cases
   * @name V10CasesDetail
   * @request GET:/api/v1.0/Cases/{id}
   * @secure
   */
  v10CasesDetail = (id: number, params: RequestParams = {}) =>
    this.request<CaseDto, ErrorDetails>({
      path: `/api/v1.0/Cases/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesDelete
   * @request DELETE:/api/v1.0/Cases/{id}
   * @secure
   */
  v10CasesDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, ErrorDetails>({
      path: `/api/v1.0/Cases/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesEmailPreviewDetail
   * @request GET:/api/v1.0/Cases/{id}/EmailPreview
   * @secure
   */
  v10CasesEmailPreviewDetail = (id: number, params: RequestParams = {}) =>
    this.request<EmailPreviewDto, ErrorDetails>({
      path: `/api/v1.0/Cases/${id}/EmailPreview`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesUpdatebusinesslineCreate
   * @request POST:/api/v1.0/Cases/updatebusinessline
   * @secure
   */
  v10CasesUpdatebusinesslineCreate = (data: UpdateBusinessLineInputDto, params: RequestParams = {}) =>
    this.request<CaseDto, ErrorDetails>({
      path: `/api/v1.0/Cases/updatebusinessline`,
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
   * @tags Cases
   * @name V10CasesCaseByIdWithAssignmentDetail
   * @request GET:/api/v1.0/Cases/caseByIdWithAssignment/{id}
   * @secure
   */
  v10CasesCaseByIdWithAssignmentDetail = (id: number, params: RequestParams = {}) =>
    this.request<CaseWithAssignmentDto, ErrorDetails>({
      path: `/api/v1.0/Cases/caseByIdWithAssignment/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesCaseAllWithAssignmentList
   * @request GET:/api/v1.0/Cases/caseAllWithAssignment
   * @secure
   */
  v10CasesCaseAllWithAssignmentList = (params: RequestParams = {}) =>
    this.request<CaseWithAssignmentDto[], ErrorDetails>({
      path: `/api/v1.0/Cases/caseAllWithAssignment`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Cases
   * @name V10CasesGuardianshipsList
   * @request GET:/api/v1.0/Cases/guardianships
   * @secure
   */
  v10CasesGuardianshipsList = (
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
    this.request<GuardianshipDto[], ErrorDetails>({
      path: `/api/v1.0/Cases/guardianships`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
}
