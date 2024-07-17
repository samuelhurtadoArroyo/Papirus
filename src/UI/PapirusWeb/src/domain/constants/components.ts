import { text } from "stream/consumers";
import { FilterParams } from "../entities/data-contracts";

// Pagination settings
export const PAGINATION_CONSTANTS = {
  rowsPerPage: [5, 10, 25, 50],
  defaultSelectedRowsPerPage: 10,
  defaultFirstPage: 1,
  firstIndex: 0,
};

// Toast notification settings
export const TOAST_CONSTANTS = {
  duration: 5000, // Display time in seconds (5s)
  delay: 300, // Delay before showing in seconds (0.3s)
};

// Input behavior settings
export const INPUT_CONSTANTS = {
  debounceDelay: 400, // Debounce delay in seconds (0.4s)
  minLengthToActivate: 1, // Min input length to trigger actions
};

export const TABLE_FILTER_CONSTANTS = {
  matchModeOptions: {
    text: [
      "IsEqualTo",
      "IsNotEqualTo",
      "StartsWith",
      "EndsWith",
      "Contains",
      "DoesNotContain",
    ],
    compare: [
      "IsEqualTo",
      "IsNotEqualTo",
      "IsGreaterThan",
      "IsGreaterThanOrEqualTo",
      "IsLessThan",
      "IsLessThanOrEqualTo",
    ],
    equals: ["IsEqualTo", "IsNotEqualTo"],
    list: ["IsEmpty", "IsNotEmpty"],
    null: ["IsNull", "IsNotNull"],
  },
  defaultMatchMode: "Contains" as FilterParams["filterOption"],
  dropdownEmptyIndentifier: -1,
};

export const TABLE_SORT_CONSTANTS = {
  defaultSortField: "id", // Default field to sort by
  defaultSortOrder: 1, //ascending
};

export const DATES = {
  localeDateString: "es-CO", //date format español-Colombia
};

export const FILE_TYPES = {
  text: "text/plain",
  html: "text/html",
  css: "text/css",
  js: "application/javascript",
  json: "application/json",
  xml: "application/xml",
  pdf: "application/pdf",
  jpeg: "image/jpeg",
  jpg: "image/jpg",
  png: "image/png",
  gif: "image/gif",
  bmp: "image/bmp",
  webp: "image/webp",
  mp3: "audio/mpeg",
  wav: "audio/wav",
  audioOgg: "audio/ogg",
  mp4: "video/mp4",
  webm: "video/webm",
  videoOgg: "video/ogg",
  ttf: "font/ttf",
  woff: "font/woff",
  woff2: "font/woff2",
};

export const DOCUMENTS = {
  defaultDocumentName: "document",
  xlsm: "application/vnd.ms-excel.sheet.macroEnabled.12",
  xlsx: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
  doc: "application/msword",
  docx: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
  ppt: "application/vnd.ms-powerpoint",
  pptx: "application/vnd.openxmlformats-officedocument.presentationml.presentation",
};
