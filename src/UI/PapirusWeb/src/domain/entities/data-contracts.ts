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

export type ActionResult = object;

/** Represents an actor within the system. */
export interface ActorDto {
  /**
   * The unique identifier for the actor.
   * @format int32
   */
  id: number;
  /**
   * The type of the actor (Claimer or Defendant), as defined by the ActorTypeId enum. Defaults to ActorTypeId.Claimer.
   * @minLength 1
   */
  actorTypeId: "Claimer" | "Defendant";
  /**
   * The identifier for the person associated with this actor. Null if not associated.
   * @format int32
   */
  personId: number;
  /**
   * The  identifier for the case associated with this actor. Null if not associated.
   * @format int32
   */
  caseId: number;
}

/** Represents Business Line object. */
export interface BusinessLineDto {
  /**
   * The unique identifier of the business line.
   * @format int32
   */
  id?: number;
  /** Business line name. */
  name?: string | null;
}

export interface CaseAssignmentDto {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  caseId?: number;
  /** @format int32 */
  teamMemberId?: number;
  fullName?: string | null;
  /** @format int32 */
  statusId?: number;
  statusName?: string | null;
}

/** Represents the case document field value data transfer object. */
export interface CaseDocumentFieldValueDto {
  /**
   * The unique identifier of the case document field value.
   * @format int32
   */
  id?: number;
  /**
   * The identifier of the type document.
   * @format int32
   */
  documentTypeId?: number;
  /** The name of the document type. */
  documentTypeName?: string | null;
  /** The name of the case document field. */
  name?: string | null;
  /** The label of the case document field. */
  tag?: string | null;
  /** The value of the case document field. */
  fieldValue?: string | null;
  /**
   * The identifier of the case.
   * @format int32
   */
  caseId?: number;
}

/** Represents a case within the system */
export interface CaseDto {
  /**
   * The unique identifier for the case.
   * @format int32
   */
  id: number;
  /**
   * The registration date of the case.
   * @format date-time
   */
  registrationDate?: string | null;
  /**
   * A unique GUID identifier for the case.
   * @format uuid
   */
  guidIdentifier: string;
  /**
   * The court handling the case.
   * @minLength 1
   * @maxLength 128
   */
  court: string;
  /**
   * The city where the case is registered.
   * @minLength 1
   * @maxLength 50
   */
  city: string;
  /**
   * The amount involved in the case.
   * @format double
   */
  amount?: number | null;
  /**
   * The date the case was submitted.
   * @format date-time
   */
  submissionDate?: string | null;
  /** A unique identifier for the case submission. */
  submissionIdentifier?: string | null;
  /**
   * The deadline date for case processing.
   * @format date-time
   */
  deadLineDate?: string | null;
  /** The type of process the case is involved in. */
  processTypeId?: "Demand" | "Guardianship" | null;
  /** The email's html body. */
  emailHtmlBody?: string | null;
  /**
   * The identifier for the process this case is associated with.
   * @format int32
   */
  processId?: number | null;
  /**
   * The identifier for the subprocess this case is associated with.
   * @format int32
   */
  subProcessId?: number | null;
  /**
   * The file path to documents associated with the case.
   * @minLength 1
   */
  filePath: string;
  /**
   * The name of the file associated with the case.
   * @minLength 1
   */
  fileName: string;
  /** Indicates whether the case has been assigned. */
  isAssigned?: boolean | null;
  /**
   * Business line id associated with the case.
   * @format int32
   */
  businessLineId?: number | null;
  /** flag to know if the document has been answered */
  isAnswered?: boolean | null;
  /**
   * Date when the document has been answered
   * @format date-time
   */
  answeredDate?: string | null;
  /** flag to know where the Emergency brief has been answered */
  emergencyBriefAnswered?: boolean | null;
  /**
   * Date when the Emergency Brief has been answered
   * @format date-time
   */
  emergencyBriefAnswerDate?: string | null;
}

/** Represents a CaseProcessDocument within the system */
export interface CaseProcessDocumentDto {
  /**
   * The unique identifier for the case.
   * @format int32
   */
  id?: number;
  /**
   * The type id of the document
   * @format int32
   */
  documentTypeId?: number;
  /**
   * The process document type id
   * @format int32
   */
  processDocumentTypeId?: number;
  /**
   * The id of the case for this document
   * @format int32
   */
  caseId?: number;
  /** The name of the file */
  fileName?: string | null;
  /** The path of the file */
  filePath?: string | null;
}

/** Represents a case within the system */
export interface CaseWithAssignmentDto {
  /** Represents a case within the system */
  case?: CaseDto;
  assignment?: CaseAssignmentDto;
}

/** Represents a case within the system */
export interface EmailPreviewDto {
  /** Represents a case within the system */
  case?: CaseDto;
  /** Case documents */
  caseDocuments?: CaseDocumentFieldValueDto[] | null;
}

export interface ErrorDetails {
  errorType?: string | null;
  errors?: string[] | null;
}

export interface FilterParams {
  columnName?: string | null;
  filterValue?: string | null;
  filterOption?:
    | "StartsWith"
    | "EndsWith"
    | "Contains"
    | "DoesNotContain"
    | "IsEmpty"
    | "IsNotEmpty"
    | "IsGreaterThan"
    | "IsGreaterThanOrEqualTo"
    | "IsLessThan"
    | "IsLessThanOrEqualTo"
    | "IsEqualTo"
    | "IsNotEqualTo"
    | "IsNull"
    | "IsNotNull";
}

/** Represents a firm within the system. */
export interface FirmDto {
  /**
   * The unique identifier for the firm.
   * @format int32
   */
  id: number;
  /**
   * The name of the firm.
   * @minLength 1
   * @maxLength 50
   */
  name: string;
  /**
   * A unique GUID identifier generated for the firm. Defaults to a new GUID upon instantiation.
   * @format uuid
   */
  guidIdentifier?: string;
}

export interface GuardianshipDto {
  /**
   * The unique identifier for the guardianship case.
   * @format int32
   */
  id?: number;
  /**
   * The date when the guardianship was submitted.
   * @format date-time
   */
  submissionDate?: string | null;
  /**
   * The deadline date for processing the guardianship.
   * @format date-time
   */
  deadLineDate?: string | null;
  /** The name of the defendant in the guardianship case. */
  defendantName?: string | null;
  /** The name of the claimant in the guardianship case. */
  claimerName?: string | null;
  /** A unique identifier for the guardianship submission. */
  submissionIdentifier?: string | null;
  /** The current status of the guardianship. */
  status?: string | null;
  /**
   * The current status of the guardianship.
   * @format int32
   */
  statusId?: number;
  /**
   * The team member id assigned to this guardianship.
   * @format int32
   */
  assignedTeamMemberId?: number;
  /**
   * The user id assigned to this guardianship.
   * @format int32
   */
  memberId?: number;
  /** The team member name assigned to this guardianship. */
  assignedTeamMemberName?: string | null;
  /** Indicates whether the case has been assigned. */
  isAssigned?: boolean | null;
  /** flag to know if the document has been answered */
  isAnswered?: boolean | null;
  /**
   * Date when the document has been answered
   * @format date-time
   */
  answeredDate?: string | null;
  /** Indicate if the user is the current assigned team member. */
  isCurrentAssigned?: boolean;
  /** Indicate if was answered */
  emergencyBriefAnswered?: boolean | null;
  /**
   * Date when the brief answered was sent
   * @format date-time
   */
  emergencyBriefAnswerDate?: string | null;
}

/** Represents a user login response. */
export interface LoginDto {
  /** The authentication token provided upon successful login. */
  token?: string | null;
}

/** Represents the user's login credentials. */
export interface LoginInputDto {
  /**
   * The user's email address used for logging in.
   * @format email
   * @minLength 1
   * @maxLength 300
   */
  email: string;
  /**
   * The user's password used for logging in.
   * @minLength 1
   * @maxLength 50
   */
  password: string;
}

/** Represents a permission within the system. */
export interface PermissionDto {
  /**
   * The unique identifier for the permission.
   * @format int32
   */
  id: number;
  /**
   * The name of the permission.
   * @minLength 1
   * @maxLength 50
   */
  name: string;
  /**
   * A brief description of what the permission allows.
   * @minLength 1
   * @maxLength 300
   */
  description: string;
  /** Permission Label Code to identify */
  permissionLabelCode?: string | null;
}

/** Represents a person with identification and contact information. */
export interface PersonDto {
  /**
   * The unique identifier for the person.
   * @format int32
   */
  id: number;
  /**
   * A GUID representing the person for system-wide identification.
   * @format uuid
   */
  guidIdentifier: string;
  /**
   * The type of person (Natural/Legal)
   * @minLength 1
   */
  personTypeId: "Natural" | "Legal";
  /**
   * The name of the person.
   * @minLength 1
   * @maxLength 300
   */
  name: string;
  /**
   * The type of identification document.
   * @minLength 1
   */
  identificationTypeId:
    | "CitizenshipId"
    | "ForeignCitizenshipId"
    | "Passport"
    | "TaxIdentificationNumber"
    | "UniqueTaxRegistryNumber";
  /**
   * The identification number of the document.
   * @minLength 1
   * @maxLength 50
   */
  identificationNumber: string;
  /** The name of the support file, if any. */
  supportFileName?: string | null;
  /** The path to the support file, if any. */
  supportFilePath?: string | null;
}

/** Represents a role with a unique identifier and name. */
export interface RoleDto {
  /**
   * The unique identifier for the role.
   * @format int32
   */
  id: number;
  /**
   * The name of the role.
   * @minLength 1
   * @maxLength 50
   */
  name: string;
}

export interface SortingParams {
  sortOrder?: "Asc" | "Desc";
  columnName?: string | null;
}

/** Represents a team with a unique identifier and name. */
export interface TeamDto {
  /**
   * The unique identifier for the team.
   * @format int32
   */
  id: number;
  /**
   * The name of the team.
   * @minLength 1
   * @maxLength 50
   */
  name: string;
}

export interface TeamMemberAssignmentDto {
  /** @format int32 */
  memberId?: number;
  fullName?: string | null;
  caseLoad?: string | null;
}

/** Represents the association of a member with a team, including their role (Lead) and capacity. */
export interface TeamMemberDto {
  /**
   * The unique identifier in TeamMembers table.
   * @format int32
   */
  id?: number;
  /**
   * Team's unique identifier.
   * @format int32
   */
  teamId: number;
  /**
   * Member's unique identifier.
   * @format int32
   */
  memberId: number;
  /** Indicates whether the member is the lead of the team. */
  isLead: boolean;
  /**
   * The maximum number of cases the member can handle.
   * @format int32
   * @min 0
   * @exclusiveMin true
   */
  maxCases: number;
  /**
   * The number of cases assigned to the member.
   * @format int32
   */
  assignedCases?: number;
  /** Represents a user of the system. */
  member?: UserDto;
}

/** Represents the update action when user tries to update the business line for a case. */
export interface UpdateBusinessLineInputDto {
  /**
   * The unique identifier for the case.
   * @format int32
   */
  id?: number;
  /**
   * Business line id associated with the case.
   * @format int32
   */
  businessLineId?: number;
}

/** Represents the case document field value to update. */
export interface UpdateCaseDocumentFieldValueDto {
  /**
   * The unique identifier of the case document field value.
   * @format int32
   */
  id: number;
  /**
   * The value of the case document field.
   * @minLength 1
   */
  fieldValue: string;
}

/** Represents a user of the system. */
export interface UserDto {
  /**
   * The unique identifier for the user.
   * @format int32
   */
  id?: number;
  /** The email address of the user. */
  email?: string | null;
  /** The first name of the user. */
  firstName?: string | null;
  /** The last name of the user. */
  lastName?: string | null;
  /** The full name of the user. */
  fullName?: string | null;
  /**
   * The role of the user within the system.
   * @format int32
   */
  roleId?: number | null;
  /** Indicates whether the user is currently active. */
  isActive?: boolean;
  /**
   * The firm that the user is associated with.
   * @format int32
   */
  firmId?: number | null;
}

export interface UserInputDto {
  /**
   * The unique identifier for the user.
   * @format int32
   */
  id?: number;
  /**
   * The first name of the user.
   * @minLength 1
   * @maxLength 50
   */
  firstName: string;
  /**
   * The last name of the user.
   * @minLength 1
   * @maxLength 50
   */
  lastName: string;
  /**
   * The email address of the user.
   * @format email
   * @minLength 1
   * @maxLength 300
   */
  email: string;
  /**
   * The password for the user's account with at least one special character,
   * one upper case letter, one lower case letter, one number and at least 8 characters.
   */
  password?: string | null;
  /**
   * The role of the user within the system.
   * @format int32
   */
  roleId?: number | null;
}
