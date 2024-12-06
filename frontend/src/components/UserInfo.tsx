import LoginDialogButton from "./LoginDialogButton";
import { useQuery } from "@tanstack/react-query";
import { getUserInfo } from "~/api/getUserInfo";
import { Avatar, AvatarFallback } from "~/components/ui/avatar";
import { REACT_QUERY_USER_INFO_KEY } from "~/lib/consts";

const UserInfo = () => {
  const { data } = useQuery({
    queryKey: [REACT_QUERY_USER_INFO_KEY],
    queryFn: getUserInfo,
  });

  if (!data) {
    return <LoginDialogButton />;
  }

  // TODO: add some profile options on avatar click

  return (
    <div className="flex flex-row">
      <Avatar>
        <AvatarFallback>
          {data?.firstName[0]?.toUpperCase() ?? ""}
        </AvatarFallback>
      </Avatar>
    </div>
  );
};

export default UserInfo;
