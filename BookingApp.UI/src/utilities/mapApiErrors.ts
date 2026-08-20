export default function mapApiErrors(errors: Record<string, string[]>){
    
    const toCamelCase = (str: string) => 
        str.charAt(0).toLowerCase() + str.slice(1);
    
    return Object.fromEntries(
        Object.entries(errors).map(([key, value]) => [toCamelCase(key), value])
    );
};