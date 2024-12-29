"use client";

import CompleteRegistrationForm from "~/components/forms/CompleteRegistrationForm";

const CompleteRegistration = () => {
  return (
    <div className="flex min-h-screen w-full items-center justify-center">
      <div className="w-4/5 space-y-12 lg:w-[600px]">
        <h1 className="text-3xl font-semibold">Complete your profile info</h1>
        <CompleteRegistrationForm />
      </div>
    </div>
  );
};

export default CompleteRegistration;
