"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { Loader2, Upload, X } from "lucide-react";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { useToast } from "~/hooks/use-toast";
import {
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  Form,
  FormDescription,
} from "../ui/form";
import { Button } from "../ui/button";
import { useMutation } from "@tanstack/react-query";
import {
  employeeConfirmReturn,
  type IEmployeeConfirmReturnProps,
} from "~/api/employeeConfirmReturn";

interface IEmployeeConfirmReturnFormProps {
  rentId: number;
}

const formSchema = z.object({
  rentId: z.number(),
  photos: z
    .any()
    .optional()
    .refine((files) => {
      if (!files) return true;
      if (typeof window === "undefined") return true;
      return files instanceof FileList;
    }, "Must be a FileList"),
});

type FormSchemaType = z.infer<typeof formSchema>;

const EmployeeConfirmReturnForm = (props: IEmployeeConfirmReturnFormProps) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      photos: undefined,
      rentId: props.rentId,
    },
  });

  const { toast } = useToast();
  const router = useRouter();

  const mutation = useMutation({
    mutationFn: (values: IEmployeeConfirmReturnProps) =>
      employeeConfirmReturn(values),
    onSuccess: () => {
      toast({
        title: "Return confirmed!",
        variant: "success",
      });
      router.push("/employee");
    },
    onError: (error) =>
      toast({
        title: "Failed to confirm return!",
        description: `An error occured while trying to confirm the return (${error.message}). Try again later.`,
        variant: "destructive",
      }),
  });

  const handleSubmit = (data: FormSchemaType) => {
    mutation.mutate({ rentId: data.rentId, photos: data.photos as FileList });
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(handleSubmit)}
        className="w-full space-y-6"
      >
        <FormField
          control={form.control}
          name="photos"
          render={({ field: { onChange, value, ...field } }) => (
            <FormItem>
              <FormLabel>Upload Car Photos</FormLabel>
              <div className="flex flex-col gap-4">
                <input
                  type="file"
                  multiple
                  accept="image/*"
                  className="hidden"
                  id="photo-upload"
                  onChange={(e) => {
                    const files = e.target.files;
                    if (files) {
                      onChange(files);
                    }
                  }}
                  {...field}
                />
                <label
                  htmlFor="photo-upload"
                  className="flex w-fit cursor-pointer items-center gap-2 rounded-md bg-secondary px-4 py-2 text-secondary-foreground hover:bg-secondary/80"
                >
                  <Upload className="h-4 w-4" />
                  Choose
                </label>
                {value && (
                  <div className="flex w-full flex-row items-center space-x-5">
                    <div className="text-sm text-muted-foreground">
                      {(value as FileList).length} file(s) selected
                    </div>
                    <Button
                      type="button"
                      size="icon"
                      variant="destructive"
                      className="h-8 w-8 p-0"
                      onClick={() => {
                        onChange(undefined);
                        // Also reset the input value
                        const input = document.getElementById(
                          "photo-upload",
                        ) as HTMLInputElement;
                        if (input) input.value = "";
                      }}
                    >
                      <X className="h-4 w-4" />
                    </Button>
                  </div>
                )}
              </div>
              <FormMessage />
              <FormDescription>Uploading photos is optional</FormDescription>
            </FormItem>
          )}
        />
        <div className="flex w-full items-center justify-center">
          <Button type="submit" disabled={mutation.isPending}>
            {mutation.isPending && <Loader2 className="animate-spin" />}
            Confirm Return
          </Button>
        </div>
      </form>
    </Form>
  );
};

export default EmployeeConfirmReturnForm;
