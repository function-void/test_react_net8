/** @type {import('tailwindcss').Config} */

module.exports = {
    mode: 'jit',
    important: true,
    content: [
        "./src/**/*.{js,jsx,ts,tsx}",
    ],
    theme: {
        extend: {
            fontFamily: {
                'sans': ["'Roboto'", 'sans-serif'],
            },
            colors: {
                np: {
                    primary: '#1565c0',
                },
                em: {
                    primary: '#388e3c'
                },
                admin: {
                    primary: '#f60'
                }
            }
        },
        screens: {
            'esm': '340px',
            'sm': '640px',
            'md': '768px',
            'lg': '1024px',
            'xl': '1280px',
            '2xl': '1536px',
        }
    },
    variants: {
        fill: ['hover', 'focus'],
    },
    plugins: [
        require('tailwindcss/nesting')
    ],
    safelist: [
        'text-np-primary',
        'text-em-primary',
        'fill-np-primary',
        'fill-em-primary',
        'hover:text-np-primary',
        'hover:text-em-primary',
        'group-hover:text-np-primary',
        'group-hover:text-em-primary',
        'group-disabled:text-np-primary',
        'group-disabled:text-em-primary',
        'bg-np-primary',
        'bg-em-primary',
        'hover:bg-np-primary',
        'hover:bg-em-primary',
        'hover:border-np-primary',
        'hover:border-em-primary',
        'border-np-primary',
        'border-em-primary',
        'border-b-np-primary',
        'border-b-em-primary',
        'hover:ring-np-primary/50',
        'hover:ring-em-primary/50',
        'disabled:ring-np-primary/50',
        'disabled:ring-em-primary/50',
        'checked:bg-np-primary',
        'checked:bg-em-primary',
        'text-[#d8d8d8]',
        'text-[#f60]',
        'hover:text-[#f60]',
        'group-hover:text-[#f60]',
        'w-[30%]',
        'w-[50%]',
        'w-[70%]',
        'text-purple-600',
        'hover:text-purple-600',
        'text-orange-500',
        'hover:text-orange-500',
        'text-red-500',
        'bg-red-500',
        'disabled:border-gray-300',
        'text-[#7e3af2]',
        'hover:text-[#7e3af2]',
        'text-[#ff5a1f]',
        'hover:text-[#ff5a1f]',
        'group-hover:text-[#7e3af2]',
        'group-hover:text-[#ff5a1f]',
    ]
};

