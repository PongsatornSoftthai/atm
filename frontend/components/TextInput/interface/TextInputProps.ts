interface TextInputProps {
    id: any;
    label?: string;
    value?: string;
    type?: "number"|"password"|"text";
    onChange?: (data:any) => void;
}