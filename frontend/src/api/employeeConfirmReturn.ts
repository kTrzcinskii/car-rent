import axios from "axios";
import {
  EMPLOYEE_API_BASE_URL,
  EMPLOYEE_TOKEN_KEY,
  EMPLOYEE_API_KEY,
} from "~/lib/consts";

export interface IEmployeeConfirmReturnProps {
  rentId: number;
  photos?: FileList;
}

export const employeeConfirmReturn = async (
  props: IEmployeeConfirmReturnProps,
) => {
  const url = `${EMPLOYEE_API_BASE_URL}/rent/worker/confirm-return`;
  const token = sessionStorage.getItem(EMPLOYEE_TOKEN_KEY);

  const data = new FormData();
  data.append("rentId", props.rentId.toString());
  if (props.photos) {
    // eslint-disable-next-line @typescript-eslint/prefer-for-of
    for (let i = 0; i < props.photos.length; i++) {
      data.append("photos", props.photos[i]!);
    }
  }

  await axios.put(url, data, {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "multipart/form-data",
      "x-api-key": EMPLOYEE_API_KEY,
    },
  });
};
